public interface SceneControllerInterface {
    bool isDesktopMousePointerEnabled();
    void onClickDesktopMouseLeftButton();
    void onClickDesktopMouseScrollButton();
    bool isCardboardGazeEnabled();
    void onTouchCardboardScreen();
    bool isDaydreamPointerEnabled();
    void onClickDaydreamTouchpadButton();
    void onClickDaydreamAppButton();
}