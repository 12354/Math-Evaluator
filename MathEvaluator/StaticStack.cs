using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEvaluator
{
    public class StaticStack<T> : List<T>
    {
        private int m_Top;
        public StaticStack(){}
        public StaticStack(IEnumerable<T> collection)
            : base(collection)
        {
            ResetTop();
        }
        public int StackCount
        {
            get
            {
                return m_Top;
            }
        }
        public void Push(T obj)
        {
            Add(obj);
            m_Top = Count - 1;
        }
        public void Push(params T[] objs)
        {
            foreach(T obj in objs)
            {
                Add(obj);
                m_Top = Count - 1;
            }
        }
        public T Pop()
        {
            return this[m_Top--];
        }
        public T PopReal()
        {
            T obj = this[m_Top];
            this.RemoveAt(m_Top);
            ResetTop();
            return obj;
        }
        public T Peek()
        {
            return this[m_Top];
        }
        public T PeekBottom()
        {
            return this[0];
        }
        public void ResetTop()
        {
            m_Top = Count - 1;
        }

    }
}
