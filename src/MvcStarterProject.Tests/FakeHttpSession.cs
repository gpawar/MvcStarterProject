using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using HttpInterfaces;

namespace MvcStarterProject.Tests
{
    public class FakeHttpSession : IHttpSession
    {
        private readonly SessionStateItemCollection _dictionary = new SessionStateItemCollection();

        public IEnumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_dictionary).CopyTo(array, index);
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public object SyncRoot
        {
            get { return ((ICollection)_dictionary).SyncRoot; }
        }

        public bool IsSynchronized
        {
            get { return ((ICollection)_dictionary).IsSynchronized; }
        }

        public void Abandon()
        {
            _dictionary.Clear();
        }

        public void Add(string name, object value)
        {
            _dictionary[name] = value;
        }

        public void Remove(string name)
        {
            _dictionary.Remove(name);
        }

        public void RemoveAt(int index)
        {
            _dictionary.RemoveAt(index);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public void RemoveAll()
        {
            _dictionary.Clear();
        }

        public object this[string key]
        {
            get { return _dictionary[key]; }
            set { _dictionary[key] = value; }
        }

        public string SessionID { get; set; }
        public int Timeout { get; set; }
        public bool IsNewSession { get; set; }
        public SessionStateMode Mode { get; set; }
        public bool IsCookieless { get; set; }
        public HttpCookieMode CookieMode { get; set; }
        public int LCID { get; set; }
        public int CodePage { get; set; }
        public IHttpSession Contents
        {
            get { return this; }
        }

        public HttpStaticObjectsCollection StaticObjects { get; set; }
        public NameObjectCollectionBase.KeysCollection Keys
        {
            get { return _dictionary.Keys; }
        }

        public bool IsReadOnly { get; set; }
    }
}