namespace TremendBoard.Mvc
{
    public class Number
    {
        private int _number;
        public Number(int number)
        {
            _number = number;
        }
        public string IsEven()
        {
            if (_number % 2 == 0)
                return "yes";
            else return "no";
        }

        //public string IsPrime(int number)
        //{

        //}

    }
}
