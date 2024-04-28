namespace DataPoolLib
{
    public static class PoolDuplicatesEx
    {
        public static PoolBase<double> RemoveDuplicates(PoolBase<double> matrix)
        {
            int numComponents = matrix.NumComponents;

            int count = matrix.Count;
            PoolBase<double> clone = new PoolBase<double>(count, numComponents);

            for (int i = 0; i < count; i++)
            {
                int sourceOffset = i * numComponents;
                bool isDuplciate = false;
                for (int j = 0; j < clone.Count; j++)
                {
                    int targetOffset = j * numComponents;
                    bool shouldKeep = true;
                    for (int k = 0; k < numComponents; k++)
                    {
                        if (matrix._values[sourceOffset + k] != clone._values[targetOffset + k])
                        {
                            shouldKeep = false;
                            break;
                        }
                    }

                    if (shouldKeep)
                    {
                        isDuplciate = true;
                        break;
                    }
                }

                if (false == isDuplciate)
                {
                    int index = clone.IndexForNewItem();
                    for (int k = 0; k < numComponents; k++)
                    {
                        clone._values[index * numComponents + k] = matrix._values[sourceOffset + k];
                    }
                }
            }
            return clone;
        }

        public static PoolBase<int> RemoveDuplicates(PoolBase<int> matrix)
        {
            int numComponents = matrix.NumComponents;

            int count = matrix.Count;
            PoolBase<int> clone = new PoolBase<int>(count, numComponents);

            for (int i = 0; i < count; i++)
            {
                int sourceOffset = i * numComponents;
                bool isDuplciate = false;
                for (int j = 0; j < clone.Count; j++)
                {
                    int targetOffset = j * numComponents;
                    bool shouldKeep = true;
                    for (int k = 0; k < numComponents; k++)
                    {
                        if (matrix._values[sourceOffset + k] != clone._values[targetOffset + k])
                        {
                            shouldKeep = false;
                            break;
                        }
                    }

                    if (shouldKeep)
                    {
                        isDuplciate = true;
                        break;
                    }
                }

                if (false == isDuplciate)
                {
                    int index = clone.IndexForNewItem();
                    for (int k = 0; k < numComponents; k++)
                    {
                        clone._values[index * numComponents + k] = matrix._values[sourceOffset + k];
                    }
                }
            }
            return clone;
        }
    }
}
