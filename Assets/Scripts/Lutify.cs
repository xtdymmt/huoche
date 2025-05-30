// dnSpy decompiler from Assembly-CSharp.dll class: Lutify
using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Lutify")]
public class Lutify : MonoBehaviour
{
	public bool IsLinear
	{
		get
		{
			return QualitySettings.activeColorSpace == ColorSpace.Linear;
		}
	}

	public Material Material
	{
		get
		{
			if (this.m_Use2DLut || this.ForceCompatibility)
			{
				if (this.m_Material2D == null)
				{
					this.m_Material2D = new Material(this.Shader2D);
					this.m_Material2D.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_Material2D;
			}
			if (this.m_Material3D == null)
			{
				this.m_Material3D = new Material(this.Shader3D);
				this.m_Material3D.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_Material3D;
		}
	}

	protected virtual void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			UnityEngine.Debug.LogWarning("Image effects aren't supported on this device");
			base.enabled = false;
			return;
		}
		if (!SystemInfo.supports3DTextures)
		{
			this.m_Use2DLut = true;
		}
		if ((!this.m_Use2DLut && (!this.Shader3D || !this.Shader3D.isSupported)) || (this.m_Use2DLut && (!this.Shader2D || !this.Shader2D.isSupported)))
		{
			UnityEngine.Debug.LogWarning("The shader is null or unsupported on this device");
			base.enabled = false;
		}
	}

	protected virtual void OnEnable()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 1)
		{
			this.LutTexScript = (LutTex)UnityEngine.Object.FindObjectOfType(typeof(LutTex));
			if (this.LutTexScript.LookupTexture != null)
			{
				this.LookupTexture = this.LutTexScript.LookupTexture;
			}
		}
		if (this.LookupTexture != null && this.LookupTexture.GetInstanceID() != this.m_BaseTextureIntanceID)
		{
			this.ConvertBaseTexture3D();
		}
	}

	protected virtual void OnDisable()
	{
		if (this.m_Material2D)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material2D);
		}
		if (this.m_Material3D)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material3D);
		}
		if (this.m_Lut3D)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Lut3D);
		}
		this.m_BaseTextureIntanceID = -1;
	}

	protected void SetIdentityLut3D()
	{
		int num = 16;
		Color[] array = new Color[num * num * num];
		float num2 = 1f / (1f * (float)num - 1f);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array[i + j * num + k * num * num] = new Color((float)i * num2, (float)j * num2, (float)k * num2, 1f);
				}
			}
		}
		if (this.m_Lut3D)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Lut3D);
		}
		this.m_Lut3D = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		this.m_Lut3D.hideFlags = HideFlags.HideAndDontSave;
		this.m_Lut3D.SetPixels(array);
		this.m_Lut3D.Apply();
		this.m_BaseTextureIntanceID = -1;
	}

	public bool ValidDimensions(Texture2D tex2D)
	{
		return !(tex2D == null) && tex2D.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2D.width));
	}

	protected void ConvertBaseTexture3D()
	{
		if (!this.ValidDimensions(this.LookupTexture))
		{
			UnityEngine.Debug.LogWarning("The given 2D texture " + this.LookupTexture.name + " cannot be used as a LUT. Pick another texture or adjust dimension to e.g. 256x16.");
			return;
		}
		this.m_BaseTextureIntanceID = this.LookupTexture.GetInstanceID();
		int height = this.LookupTexture.height;
		Color[] pixels = this.LookupTexture.GetPixels();
		Color[] array = new Color[pixels.Length];
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < height; j++)
			{
				for (int k = 0; k < height; k++)
				{
					int num = height - j - 1;
					array[i + j * height + k * height * height] = pixels[k * height + i + num * height * height];
				}
			}
		}
		if (this.m_Lut3D)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Lut3D);
		}
		this.m_Lut3D = new Texture3D(height, height, height, TextureFormat.ARGB32, false);
		this.m_Lut3D.hideFlags = HideFlags.HideAndDontSave;
		this.m_Lut3D.wrapMode = TextureWrapMode.Clamp;
		this.m_Lut3D.SetPixels(array);
		this.m_Lut3D.Apply();
	}

	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.LookupTexture == null || this.Blend <= 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		int num = 0;
		if (this.Split == Lutify.SplitMode.Horizontal)
		{
			num = 2;
		}
		else if (this.Split == Lutify.SplitMode.Vertical)
		{
			num = 4;
		}
		if (this.IsLinear)
		{
			num++;
		}
		if (this.m_Use2DLut || this.ForceCompatibility)
		{
			this.RenderLut2D(source, destination, num);
		}
		else
		{
			this.RenderLut3D(source, destination, num);
		}
	}

	private void RenderLut3D(RenderTexture source, RenderTexture destination, int pass)
	{
		if (this.LookupTexture.GetInstanceID() != this.m_BaseTextureIntanceID)
		{
			this.ConvertBaseTexture3D();
		}
		if (this.m_Lut3D == null)
		{
			this.SetIdentityLut3D();
		}
		this.m_Lut3D.filterMode = this.LutFiltering;
		float num = (float)this.m_Lut3D.width;
		this.Material.SetTexture("_LookupTex3D", this.m_Lut3D);
		this.Material.SetVector("_Params", new Vector3((num - 1f) / num, 1f / (2f * num), this.Blend));
		Graphics.Blit(source, destination, this.Material, pass);
	}

	private void RenderLut2D(RenderTexture source, RenderTexture destination, int pass)
	{
		this.LookupTexture.filterMode = this.LutFiltering;
		float num = Mathf.Sqrt((float)this.LookupTexture.width);
		this.Material.SetTexture("_LookupTex2D", this.LookupTexture);
		this.Material.SetVector("_Params", new Vector4(1f / (float)this.LookupTexture.width, 1f / (float)this.LookupTexture.height, num - 1f, this.Blend));
		Graphics.Blit(source, destination, this.Material, pass + ((this.LutFiltering != FilterMode.Point) ? 0 : 6));
	}

	[Tooltip("The texture to use as a lookup table. Size should be: height = sqrt(width)")]
	public Texture2D LookupTexture;

	private LutTex LutTexScript;

	[Tooltip("Shows a before/after comparison by splitting the screen in half.")]
	public Lutify.SplitMode Split;

	[Tooltip("Lutify will automatically detect the correct shader to use for the device but you can force it to only use the compatibility shader.")]
	public bool ForceCompatibility;

	[Tooltip("Sets the filter mode for the LUT texture. You'll want to set this to Point when using palette reduction LUTs.")]
	public FilterMode LutFiltering = FilterMode.Bilinear;

	[Range(0f, 1f)]
	[Tooltip("Blending factor.")]
	public float Blend = 1f;

	public int _LastSelectedCategory;

	public int _ThumbWidth = 110;

	public int _ThumbHeight;

	private int cache_ThumbWidth;

	private int cache_ThumbHeight;

	private bool cache_IsLinear;

	public RenderTexture _PreviewRT;

	protected Texture3D m_Lut3D;

	protected int m_BaseTextureIntanceID;

	protected bool m_Use2DLut;

	public Shader Shader2D;

	public Shader Shader3D;

	protected Material m_Material2D;

	protected Material m_Material3D;

	public enum SplitMode
	{
		None,
		Horizontal,
		Vertical
	}
}
