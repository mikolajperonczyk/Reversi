namespace Projekt_PO2_Reversi
{
    internal class Human : Gracz
    {
        TaskCompletionSource<Pole> tcs;

        public override Pole TCS
        {
            get { return null; }
            set
            {
                if (tcs.Task.IsCompleted)
                    tcs = new TaskCompletionSource<Pole>();
                tcs.SetResult(value);
            }
        }

        public Human(int color) : base(color)
        {
            tcs = new TaskCompletionSource<Pole>();
        }

        public override Pole Ruch(List<Pole> dostepne)
        {
            Pole res;
            do
            {
                res = NewTask().Result;
            } while (!dostepne.Contains(res));

            return res;
        }

        public Task<Pole> NewTask()
        {
            tcs = new TaskCompletionSource<Pole>();
            return tcs.Task;
        }
    }
}