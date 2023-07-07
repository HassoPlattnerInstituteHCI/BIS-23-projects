using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equalizer : MonoBehaviour {
	public AudioAnalyzer analyzer;
	public GameObject equalizerBarPrefab;
	public GameObject barPrefab;
	public GameObject knobPrefab;
	public float barYScale = 0.43f;

	//added
	public float barZScale = 0.43f;
	public float eqoffsetinZ = 0;


	private float barXScale = 1f;
	private float columnScale = 1.1f;
	public float maxXCoordinate = 17;
	public Gradient barColorsGradient;
	public EqualizerBar[] bars;
	public AudioFilterPeakingFilter filter;
	// Use this for initialization
	void Start () {
		int num = analyzer.numOfBands;
		bars = new EqualizerBar[num];

		columnScale = maxXCoordinate / num;
		barXScale = columnScale * 0.92f;

		LineRenderer lr = GetComponent<LineRenderer>();
		lr.positionCount = num;
		lr.colorGradient = barColorsGradient;

		for (int i = 0; i < num; i++) {
			GameObject go = GameObject.Instantiate(equalizerBarPrefab) as GameObject;
			go.transform.SetParent(this.transform);
			//added
			//go.transform.localPosition = Vector3.right * (i-num/2) * columnScale;
			go.transform.localPosition = Vector3.right * (i - num / 2) * columnScale + Vector3.back*eqoffsetinZ;

			EqualizerBar bar = go.GetComponent<EqualizerBar>();
			bars[i] = bar;
			bar.barPrefab = barPrefab;
			//changed
			bar.barYScale = barYScale;
			bar.barZScale = barZScale;

			bar.barXScale = barXScale;
			bar.baseColor = barColorsGradient.Evaluate(Mathf.InverseLerp(0,num,i));
			//// knob position
			//Vector3 knobPosition = new Vector3((i-num/2) * columnScale, //x
			//									0,						//y orifginal: barYScale*7.5f,	
			//									barYScale * -2f);						//z original: -7f
			////lr.SetPosition(i, knobPosition);
			//GameObject knob = GameObject.Instantiate(knobPrefab) as GameObject;
			//knob.transform.SetParent(this.transform);
			////knobPosition.z = -0.8f;
			//knob.transform.localPosition = knobPosition;
			//lr.SetPosition(i, knob.transform.position);
			//knob.GetComponent<EqKnob>().id = i;

			//new
			Vector3 knobPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, go.transform.localPosition.z);

			lr.SetPosition(i, knobPosition);

			GameObject knob = GameObject.Instantiate(knobPrefab) as GameObject;
			knob.transform.localPosition = knobPosition;
			knob.GetComponent<EqKnob>().id = i;

		}
		AudioAnalyzer.spectrumBarsUpdated += UpdateVisuals;
	}

	bool knobClicked = false;
	Transform knobClickedTransform = null;
	void UpdateVisuals()
	{
		for (int i = 0; i < analyzer.numOfBands; i++) {
			float value = analyzer.GetVisualScale(i);
			float decayedValue = analyzer.GetVisualScaleDecayed(i);
			bars[i].UpdateBar(value, decayedValue);
		}


		if (Input.GetMouseButtonDown(0)) {
        	//Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (hit.collider != null && hit.collider.gameObject.tag == "knob") {
					knobClicked = true;
					knobClickedTransform = hit.collider.gameObject.transform;
					knobClickedTransform.gameObject.GetComponent<Renderer>().material.color = Color.red;
				}
			}
        }
		if (Input.GetMouseButtonUp(0)) {
			if (knobClickedTransform!=null) knobClickedTransform.gameObject.GetComponent<Renderer>().material.color = Color.grey;
			knobClicked = false;
			knobClickedTransform = null;
		}
		if (Input.GetMouseButton(0)) {
			if (knobClicked) {
				float newYPosition = 0;
				Vector3 v3 = Input.mousePosition;
				v3.z = (Camera.main.transform.position - knobClickedTransform.position).magnitude;
				v3 = Camera.main.ScreenToWorldPoint(v3);
				newYPosition = v3.y;
				float minYPos = barYScale/2;
				float maxYPos = barYScale*15 - barYScale/2;
				newYPosition = Mathf.Clamp(newYPosition, minYPos, maxYPos);
				knobClickedTransform.position = new Vector3(knobClickedTransform.position.x, newYPosition, 
					knobClickedTransform.position.z);
				int i = knobClickedTransform.GetComponent<EqKnob>().id;
				Vector3 lrPos = new Vector3((i-analyzer.numOfBands/2) * columnScale, newYPosition, -7f);
				LineRenderer lr = GetComponent<LineRenderer>();
				lr.SetPosition(i, lrPos);

				if (filter) {
					filter.dbGain[i] = Mathf.Lerp(-24f, 24f, (newYPosition-minYPos) / (maxYPos-minYPos));
					filter.Reprogram();
				}
			}
		}
	}
}

