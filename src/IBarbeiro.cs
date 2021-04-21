namespace barbeiro_dorminhoco
{
    public interface IBarbeiro
    {
        void StartWorking();
        void StopWorking();
        int TempoDormido {get;}
    }
}