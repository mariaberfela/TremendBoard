namespace TremendBoard.Mvc
{
    public class Multiplication
    {
        private int _num1;
        private int _num2;

        public Multiplication(int num1, int num2)
        {
            _num1 = num1;
            _num2 = num2;
        }

        public int GetMult()
        {
            return _num1 * _num2;
        }

        public bool AreEqual()
        {
            if(_num1 == _num2)
            {
                return true;

            }
            return false;
        }
    }
}
