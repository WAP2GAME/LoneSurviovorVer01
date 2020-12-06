


public interface IGameOverDispatcher
{
    void NotifyGameOver();
    bool AddObserver(IGameOverObserver observer);
    void DeleteObserver(IGameOverObserver observer);
}

public interface IGameOverObserver
{
   void EndStage();
}



