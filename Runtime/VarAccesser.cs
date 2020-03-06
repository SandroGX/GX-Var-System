namespace GX.VarSystem
{
    //Only works on Unity 2020.1 and above because of generic serialization, use the VarIdx attribute on an int instead and use VarHandle.Get<V>(int) and VarHandle.Set<V>(int, V)
    [System.Serializable]
    public struct VarAccesser<V>
    {
        public int idx;

        //public VarAccesser() { }
        public VarAccesser(int idx) { this.idx = idx; }
        public V this[VarHandle v]
        {
            get => v.Get<V>(idx);
            set => v.Set(idx, value);
        }

        public System.Type GetVarType()
        {
            return typeof(V);
        }

        public static implicit operator int(VarAccesser<V> v) { return v.idx; }
    }
}
