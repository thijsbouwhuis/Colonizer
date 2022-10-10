using System;

namespace Pathfinding
{
    public class PathfindingHeap<T> where T : IPathfindingHeapItem<T>
    {
        private T[] items;
        private int curItemCount;

        public PathfindingHeap(int maxSize)
        {
            items = new T[maxSize];
        }

        public void Add(T item)
        {
            item.Index = curItemCount;
            items[curItemCount] = item;
            SortUpwards(item);
            curItemCount++;
        }

        public T RemoveFirst()
        {
            T first = items[0];
            curItemCount--;

            items[0] = items[curItemCount];
            items[0].Index = 0;
            SortDown(items[0]);
            return first;
        }

        public bool Contains(T item)
        {
            return Equals(items[item.Index], item);
        }

        public int GetCount()
        {
            return curItemCount;
        }

        public void UpdateItem(T item)
        {
            SortUpwards(item);
        }

        void SortDown(T item)
        {
            while (true)
            {
                int childL = item.Index * 2 + 1;
                int childR = item.Index * 2 + 2;
                int swapIndex = 0;

                if (childL < curItemCount)
                {
                    swapIndex = childL;
                    if (childR < curItemCount)
                    {
                        if (items[childL].CompareTo(items[childR]) < 0)
                        {
                            swapIndex = childR;
                        }
                    }

                    if (item.CompareTo(items[swapIndex]) < 0)
                    {
                        Swap(item, items[swapIndex]);
                        continue;
                    }
                }


                break;
            }
        }

        void SortUpwards(T item)
        {
            int parentIndex = (item.Index - 1) / 2;
            while (true)
            {
                T parent = items[parentIndex];
                if (item.CompareTo(parent) > 0)
                {
                    Swap(item, parent);
                    parentIndex = (item.Index - 1) / 2;
                    continue;
                }

                break;
            }
        }

        void Swap(T a, T b)
        {
            items[a.Index] = b;
            items[b.Index] = a;
            int aIndex = a.Index;
            a.Index = b.Index;
            b.Index = aIndex;
        }
    }
}

public interface IPathfindingHeapItem<T> : IComparable<T>
{
    int Index { get; set; }
}