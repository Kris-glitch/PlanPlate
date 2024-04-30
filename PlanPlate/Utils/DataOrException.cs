namespace PlanPlate.Utils
{
    public class DataOrException<TData, EException> where EException : Exception
    {
        public TData? Data { get; set; }
        public bool Loading { get; set; }
        public EException? Exception { get; set; }
        public DataOrException()
        {
            Data = default;
            Loading = false;
            Exception = null;
        }

        public DataOrException(TData data)
        {
            Data = data;
            Loading = false;
            Exception = null;
        }

        public DataOrException(bool loading)
        {
            Data = default;
            Loading = loading;
            Exception = null;
        }

        public DataOrException(EException exception)
        {
            Data = default;
            Loading = false;
            Exception = exception;
        }
    }
}