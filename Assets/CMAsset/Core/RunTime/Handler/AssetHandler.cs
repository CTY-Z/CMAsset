using CM;
using System;
using UnityEngine;

namespace CMAsset.Runtime
{
    public delegate void AssetLoadedCallback<T>();

    public abstract class AssetHandler : BaseHandler
    {
        public object AssetObj {  get; protected set; }

        public AssetCategory Category { get; protected set; }

        internal abstract void SetAsset(object loadedAsset);

        public T AssetAs<T>()
        {
            if(AssetObj == null) return default(T);

            Type t = typeof(T);

            if(t == typeof(object)) return (T)AssetObj;

            switch (Category)
            {
                case AssetCategory.InternalBundledAsset:
                    if(typeof(UnityEngine.Object).IsAssignableFrom(t))
                    {
                        if (t == typeof(Sprite) && AssetObj is Texture2D tex)
                        {
                            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                            return (T)(object)sprite;
                        }
                        else if (t == typeof(Texture2D) && AssetObj is Sprite sprite)
                            return (T)(object)sprite.texture;
                        else
                            return (T)AssetObj;
                    }

                    Debug.Log($"AssetHandler.AssetAs获取失败，资源类别为{Category}，但是T为{t}");

                    return default(T);
                case AssetCategory.InternalRawAsset:
                    break;
                case AssetCategory.ExternalRawAsset:
                    break;
            }

            return default(T);
        }

        public override void RefClear()
        {
            base.RefClear();

        }
    }
}

