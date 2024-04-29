namespace DataPoolLib
{
    public class PoolBase<T>
    {
        public const int DEFAULT_CAPACITY = 10;

        readonly int m_itemMemberSize;
        readonly int m_numComponents;

        internal protected T[] _values;
        int _capacity;
        int _count;
        int _sizeIncrement = 10;
        bool _useAutoIncrement = true;

        internal ESort s_sortState = ESort.Undefined;
        internal int s_sortColumn = -1;

        public PoolBase(int expectedLength, int itemsInRow)
        {
            if (itemsInRow > 4 || itemsInRow < 2) throw new InvalidOperationException("Pool should have at least 2 components or maximum 4.");

            m_itemMemberSize = GetSize();
            m_numComponents = itemsInRow;

            _capacity = expectedLength;
            _values = new T[_capacity * itemsInRow];
        }

        public PoolBase(int itemsInRow) : this(DEFAULT_CAPACITY, itemsInRow)
        {

        }

        public PoolBase(PoolBase<T> other)
        {
            m_numComponents = other.m_numComponents;
            m_itemMemberSize = other.m_itemMemberSize;

            _count = other._count;
            _sizeIncrement = other._sizeIncrement;
            _capacity = other._capacity;
            _values = new T[other._values.Length];
            s_sortColumn = other.s_sortColumn;
            s_sortState = other.s_sortState;
            Buffer.BlockCopy(other._values, 0, _values, 0, _values.Length * m_itemMemberSize);
        }

        public PoolBase(T[] values, int itemsInRow)
        {
            if (itemsInRow > 4 || itemsInRow < 2) throw new InvalidOperationException();
            if (values.Length % itemsInRow != 0) throw new InvalidOperationException();

            m_itemMemberSize = GetSize();
            m_numComponents = itemsInRow;
            _count = values.Length / m_numComponents;
            _capacity = _count + _sizeIncrement;
            _values = values;
        }

        public int Count { get { return _count; } }
        public int Capacity { get { return _capacity; } }
        public int NumComponents { get { return m_numComponents; } }

        public ESort SortState { get { return s_sortState; } }
        public int SortColumn { get { return s_sortColumn; } }

        public int IncrementRate
        {
            get { return _sizeIncrement; }
            set
            {
                if (value <= 0) throw new InvalidOperationException("Increment should be greater than 0.");
                _sizeIncrement = value;
            }
        }

        public bool UseAutoIncrement
        {
            get { return _useAutoIncrement; }
            set { _useAutoIncrement = value; }
        }

        int GetSize()
        {
            int size;
            if (typeof(T) == typeof(int))
            {
                size = sizeof(int);
            }
            else if (typeof(T) == typeof(float))
            {
                size = sizeof(float);
            }
            else if (typeof(T) == typeof(double))
            {
                size = sizeof(double);
            }
            else
            {
                throw new ArgumentException($"Unsupported type: {typeof(T).Name}");
            }
            return size;
        }

        internal int IndexForNewItem()
        {
            int index = _count * m_numComponents;
            if (index >= _values.Length)
            {
                IncreaseCapacity(_useAutoIncrement ? _count * 2 : _sizeIncrement);
            }
            return _count++;
        }

        public void IncreaseCapacity(int increment)
        {
            if (increment <= 0) throw new InvalidOperationException("Increment should be greater than 0.");

            _capacity += increment;
            T[] newValues = new T[_capacity * m_numComponents];
            Buffer.BlockCopy(_values, 0, newValues, 0, _values.Length * m_itemMemberSize);
            _values = newValues;
        }

        public void Clear()
        {
            Array.Clear(_values);
            _count = 0;
        }

        public T[] ToBufferArray()
        {
            T[] newValues = new T[_count * m_numComponents];
            Buffer.BlockCopy(_values, 0, newValues, 0, newValues.Length * m_itemMemberSize);
            return newValues;
        }

        public bool TrimExcess()
        {
            if (_values.Length == _count * m_numComponents) return false;
            _values = ToBufferArray();
            return true;
        }

        public int AddRange(PoolBase<T> other)
        {
            if (m_numComponents != other.m_numComponents) throw new InvalidOperationException();

            int requiredCapacity = (_count + other._count) * m_numComponents;
            if (_values.Length < requiredCapacity)
            {
                IncreaseCapacity(requiredCapacity - _capacity);
            }

            int bytesToCopy = other._count * m_numComponents * m_itemMemberSize;
            Buffer.BlockCopy(other._values, 0, _values, _count * m_numComponents * m_itemMemberSize, bytesToCopy);
            return _count += other._count;
        }

        public T GetComponent(int index, int component)
        {
            return _values[index * m_numComponents + component];
        }

        public void SetComponent(int index, int component, T value)
        {
            _values[index * m_numComponents + component] = value;
        }

        public T this[int item, int component]
        {
            get { return GetComponent(item, component); }
            set { SetComponent(item, component, value); }
        }

        public void GetItem(int index, out T a, out T b)
        {
            if (index >= _capacity) throw new IndexOutOfRangeException();
            int off = index * m_numComponents;
            a = _values[off];
            b = _values[off + 1];
        }
        public void SetItem(int index, T a, T b)
        {
            if (index >= _capacity) throw new IndexOutOfRangeException();
            int off = index * m_numComponents;
            _values[off] = a;
            _values[off + 1] = b;
        }
        public int AddItem(T a, T b)
        {
            int index = IndexForNewItem();
            SetItem(index, a, b);
            return index;
        }

        public void GetItem(int index, out T a, out T b, out T c)
        {
            if (index >= _capacity) throw new IndexOutOfRangeException();
            if (m_numComponents != 3) throw new InvalidOperationException();

            int off = index * m_numComponents;
            a = _values[off];
            b = _values[off + 1];
            c = _values[off + 2];
        }
        public void SetItem(int index, T a, T b, T c)
        {
            if (index >= _capacity) throw new IndexOutOfRangeException();
            if (m_numComponents != 3) throw new InvalidOperationException();

            int off = index * m_numComponents;
            _values[off] = a;
            _values[off + 1] = b;
            _values[off + 2] = c;
        }
        public int AddItem(T a, T b, T c)
        {
            int index = IndexForNewItem();
            SetItem(index, a, b, c);
            return index;
        }

        public void GetItem(int index, out T a, out T b, out T c, out T d)
        {
            if (index >= _capacity) throw new IndexOutOfRangeException();
            if (m_numComponents != 4) throw new InvalidOperationException();

            int off = index * m_numComponents;
            a = _values[off];
            b = _values[off + 1];
            c = _values[off + 2];
            d = _values[off + 3];
        }
        public void SetItem(int index, T a, T b, T c, T d)
        {
            if (index >= _capacity) throw new IndexOutOfRangeException();
            if (m_numComponents != 4) throw new InvalidOperationException();

            int off = index * m_numComponents;
            _values[off] = a;
            _values[off + 1] = b;
            _values[off + 2] = c;
            _values[off + 3] = d;
        }
        public int AddItem(T a, T b, T c, T d)
        {
            int index = IndexForNewItem();
            SetItem(index, a, b, c, d);
            return index;
        }
    }
}
