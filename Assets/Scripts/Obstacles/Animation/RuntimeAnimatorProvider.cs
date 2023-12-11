using System.Collections.Generic;
using UnityEngine;

namespace STGO.Gameplay
{
    public static class RuntimeAnimatorProvider
    {
        private static List<string> _animationKeys = new List<string> { "Idle", "Destroy" };
        public static List<string> AnimationKeys => _animationKeys;

        public static void SetRuntimeAnimatorController(Animator animator, List<AnimationClip> animations)
        {
            var animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

            var clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
            animatorOverrideController.GetOverrides(clipOverrides);

            for (int i = 0; i < animations.Count; i++)
            {
                clipOverrides[_animationKeys[i]] = animations[i];
            }

            animatorOverrideController.ApplyOverrides(clipOverrides);
            animator.runtimeAnimatorController = animatorOverrideController;
        }
    }

    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }
}