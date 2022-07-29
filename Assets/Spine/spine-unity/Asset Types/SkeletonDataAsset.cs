using System;
using System.IO;
using UnityEngine;
using Spine;

namespace Spine.Unity
{
    public class SkeletonDataAsset : ScriptableObject
    {
        #region Inspector
        public AtlasAsset[] atlasAssets = new AtlasAsset[0];
#if SPINE_TK2D
		public tk2dSpriteCollectionData spriteCollection;
		public float scale = 1f;
#else
        public float scale = 0.01f;
#endif
        public TextAsset skeletonJSON;
        [SpineAnimation(includeNone: false)]
        public string[] fromAnimation = new string[0];
        [SpineAnimation(includeNone: false)]
        public string[] toAnimation = new string[0];
        public float[] duration = new float[0];
        public float defaultMix;
        public RuntimeAnimatorController controller;

        public bool IsLoaded { get { return this.skeletonData != null; } }

        void Reset()
        {
            Clear();
        }
        #endregion

        SkeletonData skeletonData;
        AnimationStateData stateData;

        #region Runtime Instantiation
        /// <summary>
        /// Creates a runtime SkeletonDataAsset.</summary>
        public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAsset atlasAsset, bool initialize, float scale = 0.01f)
        {
            return CreateRuntimeInstance(skeletonDataFile, new[] { atlasAsset }, initialize, scale);
        }

        /// <summary>
        /// Creates a runtime SkeletonDataAsset.</summary>
        public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAsset[] atlasAssets, bool initialize, float scale = 0.01f)
        {
            SkeletonDataAsset skeletonDataAsset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
            skeletonDataAsset.Clear();
            skeletonDataAsset.skeletonJSON = skeletonDataFile;
            skeletonDataAsset.atlasAssets = atlasAssets;
            skeletonDataAsset.scale = scale;

            if (initialize)
                skeletonDataAsset.GetSkeletonData(true);

            return skeletonDataAsset;
        }
        #endregion

        public void Clear()
        {
            skeletonData = null;
            stateData = null;
        }

        public SkeletonData GetSkeletonData(bool quiet)
        {
            if (skeletonJSON == null)
            {
                if (!quiet)
                    Debug.LogError("Skeleton JSON file not set for SkeletonData asset: " + name, this);
                Clear();
                return null;
            }

            // Disabled to support attachmentless/skinless SkeletonData.
            //			if (atlasAssets == null) {
            //				atlasAssets = new AtlasAsset[0];
            //				if (!quiet)
            //					Debug.LogError("Atlas not set for SkeletonData asset: " + name, this);
            //				Clear();
            //				return null;
            //			}
            //			#if !SPINE_TK2D
            //			if (atlasAssets.Length == 0) {
            //				Clear();
            //				return null;
            //			}
            //			#else
            //			if (atlasAssets.Length == 0 && spriteCollection == null) {
            //				Clear();
            //				return null;
            //			}
            //			#endif

            if (skeletonData != null)
                return skeletonData;

            AttachmentLoader attachmentLoader;
            float skeletonDataScale;
            Atlas[] atlasArray = this.GetAtlasArray();

#if !SPINE_TK2D
            attachmentLoader = (atlasArray.Length == 0) ? (AttachmentLoader)new RegionlessAttachmentLoader() : (AttachmentLoader)new AtlasAttachmentLoader(atlasArray);
            skeletonDataScale = scale;
#else
			if (spriteCollection != null) {
				attachmentLoader = new Spine.Unity.TK2D.SpriteCollectionAttachmentLoader(spriteCollection);
				skeletonDataScale = (1.0f / (spriteCollection.invOrthoSize * spriteCollection.halfTargetHeight) * scale);
			} else {
				if (atlasArray.Length == 0) {
					Reset();
					if (!quiet) Debug.LogError("Atlas not set for SkeletonData asset: " + name, this);
					return null;
				}
				attachmentLoader = new AtlasAttachmentLoader(atlasArray);
				skeletonDataScale = scale;
			}
#endif

            bool isBinary = skeletonJSON.name.ToLower().Contains(".skel");
            SkeletonData loadedSkeletonData;

            try
            {
                if (isBinary)
                    loadedSkeletonData = SkeletonDataAsset.ReadSkeletonData(skeletonJSON.bytes, attachmentLoader, skeletonDataScale);
                else
                    loadedSkeletonData = SkeletonDataAsset.ReadSkeletonData(skeletonJSON.text, attachmentLoader, skeletonDataScale);

            }
            catch (Exception ex)
            {
                if (!quiet)
                    Debug.LogError("Error reading skeleton JSON file for SkeletonData asset: " + name + "\n" + ex.Message + "\n" + ex.StackTrace, this);
                return null;

            }

            this.InitializeWithData(loadedSkeletonData);

            return skeletonData;
        }

        internal void InitializeWithData(SkeletonData sd)
        {
            this.skeletonData = sd;
            this.stateData = new AnimationStateData(skeletonData);
            FillStateData();
        }

        internal Atlas[] GetAtlasArray()
        {
            var returnList = new System.Collections.Generic.List<Atlas>(atlasAssets.Length);
            for (int i = 0; i < atlasAssets.Length; i++)
            {
                var aa = atlasAssets[i];
                if (aa == null) continue;
                var a = aa.GetAtlas();
                if (a == null) continue;
                returnList.Add(a);
            }
            return returnList.ToArray();
        }

        internal static SkeletonData ReadSkeletonData(byte[] bytes, AttachmentLoader attachmentLoader, float scale)
        {
            var input = new MemoryStream(bytes);
            var binary = new SkeletonBinary(attachmentLoader)
            {
                Scale = scale
            };
            return binary.ReadSkeletonData(input);
        }

        internal static SkeletonData ReadSkeletonData(string text, AttachmentLoader attachmentLoader, float scale)
        {
            var input = new StringReader(text);
            var json = new SkeletonJson(attachmentLoader)
            {
                Scale = scale
            };
            return json.ReadSkeletonData(input);
        }

        public void FillStateData()
        {
            if (stateData != null)
            {
                stateData.defaultMix = defaultMix;

                for (int i = 0, n = fromAnimation.Length; i < n; i++)
                {
                    if (fromAnimation[i].Length == 0 || toAnimation[i].Length == 0)
                        continue;
                    stateData.SetMix(fromAnimation[i], toAnimation[i], duration[i]);
                }
            }
        }

        public AnimationStateData GetAnimationStateData()
        {
            if (stateData != null)
                return stateData;
            GetSkeletonData(false);
            return stateData;
        }
    }

}
