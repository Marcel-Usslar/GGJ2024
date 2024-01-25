using DG.Tweening;

namespace Plugins.TweenAnimationSystem
{
    public static class TweenUtility
    {
        public static void PlayOnceInEditor(this Tween tween)
        {
#if UNITY_EDITOR
            tween.OnComplete(() =>
            {
                DG.DOTweenEditor.DOTweenEditorPreview.Stop();
                tween.Kill();
            });

            DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(tween);
            DG.DOTweenEditor.DOTweenEditorPreview.Start();
#endif
        }

        public static void StopEditorAnimations()
        {
#if UNITY_EDITOR
            DG.DOTweenEditor.DOTweenEditorPreview.Stop();
#endif
        }
    }
}