


public interface IStageEndNotifier
{
    void Notify();
    bool AddObserver(IStageEndObserver observer);
    void DeleteObserver(IStageEndObserver observer);
}

public interface IStageEndObserver
{
   void EndStage();
}



