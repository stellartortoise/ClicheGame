using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour
{
	/// <summary>
	/// Some of the CODE THANKS TO YOUTUBER Makin' Stuff Look Good
	/// </summary>
	public Transform ScannerOrigin;
	public Material EffectMaterial;
	public float ScanDistance;
	public int soundSpeed;

	[SerializeField] private Transform bat;

	private Camera _camera;

	// Demo Code
	[HideInInspector]
	public bool _scanning;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip[] clips;

	void Start()
    {
		_scanning = true;

		int num = UnityEngine.Random.Range(0, clips.Length);
		audioSource.PlayOneShot(clips[num]);
	}

    void Update()
	{
		if (_scanning)
		{
			ScanDistance += Time.deltaTime * soundSpeed;//50;
		}


		if (ScanDistance > 40f)
		{

            RaycastHit hit;

            if (Physics.Raycast(bat.transform.position, transform.TransformDirection(Vector3.down), out hit))
            {
                _scanning = true;
                ScanDistance = 0;
                ScannerOrigin.position = hit.point;

				int num = UnityEngine.Random.Range(0, clips.Length);
				audioSource.PlayOneShot(clips[num]);
            }
        }
	}
	// End Demo Code

	void OnEnable()
	{
		_camera = GetComponent<Camera>();
		_camera.depthTextureMode = DepthTextureMode.Depth;
	}

	[ImageEffectOpaque]
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		EffectMaterial.SetVector("_WorldSpaceScannerPos", ScannerOrigin.position);
		EffectMaterial.SetFloat("_ScanDistance", ScanDistance);
		RaycastCornerBlit(src, dst, EffectMaterial);
	}

	void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
	{
		// Compute Frustum Corners
		float camFar = _camera.farClipPlane;
		float camFov = _camera.fieldOfView;
		float camAspect = _camera.aspect;

		float fovWHalf = camFov * 0.5f;

		Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
		Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

		Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
		float camScale = topLeft.magnitude * camFar;

		topLeft.Normalize();
		topLeft *= camScale;

		Vector3 topRight = (_camera.transform.forward + toRight + toTop);
		topRight.Normalize();
		topRight *= camScale;

		Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
		bottomRight.Normalize();
		bottomRight *= camScale;

		Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
		bottomLeft.Normalize();
		bottomLeft *= camScale;

		// Custom Blit, encoding Frustum Corners as additional Texture Coordinates
		RenderTexture.active = dest;

		mat.SetTexture("_MainTex", source);

		GL.PushMatrix();
		GL.LoadOrtho();

		mat.SetPass(0);

		GL.Begin(GL.QUADS);

		GL.MultiTexCoord2(0, 0.0f, 0.0f);
		GL.MultiTexCoord(1, bottomLeft);
		GL.Vertex3(0.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 0.0f);
		GL.MultiTexCoord(1, bottomRight);
		GL.Vertex3(1.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 1.0f);
		GL.MultiTexCoord(1, topRight);
		GL.Vertex3(1.0f, 1.0f, 0.0f);

		GL.MultiTexCoord2(0, 0.0f, 1.0f);
		GL.MultiTexCoord(1, topLeft);
		GL.Vertex3(0.0f, 1.0f, 0.0f);

		GL.End();
		GL.PopMatrix();
	}
}
