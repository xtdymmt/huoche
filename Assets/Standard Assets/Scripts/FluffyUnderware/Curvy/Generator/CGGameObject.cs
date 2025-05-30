// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGGameObject
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[CGDataInfo("#FFF59D")]
	public class CGGameObject : CGBounds
	{
		public CGGameObject()
		{
		}

		public CGGameObject(CGGameObjectProperties properties) : this(properties.Object, properties.Translation, properties.Rotation, properties.Scale)
		{
		}

		public CGGameObject(GameObject obj) : this(obj, Vector3.zero, Vector3.zero, Vector3.one)
		{
		}

		public CGGameObject(GameObject obj, Vector3 translate, Vector3 rotate, Vector3 scale)
		{
			this.Object = obj;
			this.Translate = translate;
			this.Rotate = rotate;
			this.Scale = scale;
			if (this.Object)
			{
				this.Name = this.Object.name;
			}
		}

		public CGGameObject(CGGameObject source) : base(source)
		{
			this.Object = source.Object;
			this.Translate = source.Translate;
			this.Rotate = source.Rotate;
			this.Scale = source.Scale;
		}

		public Matrix4x4 Matrix
		{
			get
			{
				return Matrix4x4.TRS(this.Translate, Quaternion.Euler(this.Rotate), this.Scale);
			}
		}

		public override T Clone<T>()
		{
			return new CGGameObject(this) as T;
		}

		public static CGGameObject Get(CGGameObject data, GameObject obj, Vector3 translate, Vector3 rotate, Vector3 scale)
		{
			if (data == null)
			{
				return new CGGameObject(obj);
			}
			data.Object = obj;
			data.Name = ((!(obj != null)) ? null : obj.name);
			data.Translate = translate;
			data.Rotate = rotate;
			data.Scale = scale;
			return data;
		}

		public override void RecalculateBounds()
		{
			if (this.Object == null)
			{
				this.mBounds = new Bounds?(default(Bounds));
			}
			else
			{
				Renderer[] componentsInChildren = this.Object.GetComponentsInChildren<Renderer>(true);
				Collider[] componentsInChildren2 = this.Object.GetComponentsInChildren<Collider>(true);
				Bounds value;
				if (componentsInChildren.Length > 0)
				{
					value = componentsInChildren[0].bounds;
					for (int i = 1; i < componentsInChildren.Length; i++)
					{
						value.Encapsulate(componentsInChildren[i].bounds);
					}
					for (int j = 0; j < componentsInChildren2.Length; j++)
					{
						value.Encapsulate(componentsInChildren2[j].bounds);
					}
				}
				else if (componentsInChildren2.Length > 0)
				{
					value = componentsInChildren2[0].bounds;
					for (int k = 1; k < componentsInChildren2.Length; k++)
					{
						value.Encapsulate(componentsInChildren2[k].bounds);
					}
				}
				else
				{
					value = default(Bounds);
				}
				value.size = new Vector3(value.size.x * this.Scale.x, value.size.y * this.Scale.y, value.size.z * this.Scale.z);
				this.mBounds = new Bounds?(value);
			}
		}

		public GameObject Object;

		public Vector3 Translate;

		public Vector3 Rotate;

		public Vector3 Scale = Vector3.one;
	}
}
