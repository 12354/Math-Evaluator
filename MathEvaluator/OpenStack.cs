using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEvaluator
{
    public class OpenStack<T>
    {
        private class OpenStackElement<S>
        {
            public OpenStackElement<S> Previous { get; private set; }
            public S Data{get;private set;}
            public OpenStackElement(S Element,OpenStackElement<S> _Previous)
            {
                Data = Element;
                Previous = _Previous;
            }
            public S Peek(int depth)
            {
                if (depth == 0)
                    return Data;
                return Previous.Peek(--depth);
            }
        }
        private OpenStackElement<T> m_Top;
        public int Count { get; private set; }
        public void Push(T Element)
        {
            m_Top = new OpenStackElement<T>(Element, m_Top);
            Count++;
        }
        public T Pop()
        {
            if (m_Top == null)
                throw new InvalidOperationException("Stack is empty");
            OpenStackElement<T> Pop = m_Top;
            m_Top = m_Top.Previous;
            Count--;

            return Pop.Data;
        }
        public OpenStack<T> Reverse()
        {
            OpenStack<T> tmp = new OpenStack<T>();
            OpenStackElement<T> element = m_Top;
            while (element != null)
            {
                tmp.Push(element.Data);
                element = element.Previous;
            }
            return tmp;
        }
        public T Peek()
        {
            return Peek(0);
        }
        public T Peek(int depth)
        {
            if (m_Top == null)
                throw new InvalidOperationException("Stack is empty");
            return m_Top.Peek(depth);
        }
    }
}
