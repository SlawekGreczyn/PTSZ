namespace PTSZ
{
    public class Task
    {
        private int _j;
        private int _pj;
        private int _rj;
        private int _dj;

        public Task(int pj, int rj, int dj, int j)
        {
            _j = j;
            _pj = pj;
            _rj = rj;
            _dj = dj;
        }

        public int j { get { return _j; } }
        public int pj { get { return _pj; } }
        public int rj { get { return _rj; } }
        public int dj { get { return _dj; } }
    }
}
