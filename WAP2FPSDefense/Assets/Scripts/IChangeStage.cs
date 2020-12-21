public interface IStageChangeObserver
{
    void ChangeStage(StageInfoContainer stage);
}

public interface IStageChangeNotifier
{
    void Notify(StageInfoContainer stage);
}
