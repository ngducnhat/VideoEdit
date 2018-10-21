using UnityEngine;
using Nexweron.Core.MSK;

public class MSKBridgeAVPLiveCamera : MSKBridgeBase {
	
	public AVProLiveCamera avpLiveCamera;
	public MSKController mskController;

	
	private void UpdateTexture(Texture texture) {
		if (sourceTexture != texture) {
			sourceTexture = texture;

			if (mskController != null) {
				mskController.SetSourceTexture(sourceTexture);
			} else {
				Debug.LogError("MSKBridgeAVPLiveCamera | mskController = null");
			}
		}
	}
	
	private int _framesCounter = 0;

	private void Update() {
		if(avpLiveCamera != null) {
			var framesCounter = avpLiveCamera.Device.FramesTotal;
			if (framesCounter > 0) {
				UpdateTexture(avpLiveCamera.OutputTexture);
			}
			if (_sourceTexture != null) {
				if (_framesCounter != framesCounter) {
					_framesCounter = framesCounter;

					mskController.RenderOut(_texture);
				}
			}
		} else {
			Debug.LogError("MSKBridgeAVPLiveCamera | avpLiveCamera = null");
		}
	}
}
