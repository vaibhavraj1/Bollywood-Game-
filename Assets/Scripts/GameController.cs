using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Util;

public class GameController : MonoBehaviour
{
	public Text wordIndicator;
	public Text scoreIndicator;
	public Text TotalScore;
	public GameObject restartbutton;
	public GameObject retrybutton;
	public GameObject nextbutton;
	public GameObject wintext;
	public GameObject losetext;
	public GameObject nexttext;
	public GameObject keyboard;
	private BollywoodController bw;
	private string word;
	public char s;
	private char[] revealed;
	private int score = 100;
	private int total_score = 0;
	private int wronged = 0;
	private bool result = false;
	// Use this for initialization
	void Start()
	{
		total_score = 0;
		//Dictionary(words);
		bw = GameObject.Find("Main Camera").GetComponent<BollywoodController>();
		reset();
	}

	// Update is called once per frame
	void Update()
	{
		/* Move to the next word */
		if (result == true)
		{
			if (TextUtils.isAlpha(s))
			{
				/* Check for player failure */
				if (!check(s))
				{
					bool ans = false;
					if (s == 'B' && bw.perm == bw.tries)
					{
						ans = true;
					}
					if (s == 'L' && ((bw.perm == bw.tries + 3) || (bw.perm == bw.tries + 2)))
					{
						ans = true;
					}
					if (s == 'Y' && bw.perm == bw.tries + 4)
					{
						ans = true;
					}
					if (s == 'W' && bw.perm == bw.tries + 6)
					{
						ans = true;
					}
					if (s == 'D' && bw.perm == bw.tries + 9)
					{
						ans = true;
					}
					if (ans == false)
					{
						bw.wrong();
						score -= 10;
						updateScoreIndicator();
						wronged++;
						if (bw.isOver)
						{
							wordIndicator.text = "";
							lose();
						}
					}
				}
			}
			result = false;
		}
	}

	private bool check(char c)
	{
		bool ret = false;
		int complete = 0;
		int sc = 0;

		for (int i = 0; i < revealed.Length; i++)
		{
			if (c == word[i])
			{
				ret = true;
				if (revealed[i] == 0)
				{
					revealed[i] = c;
					sc++;
				}
			}
			if (revealed[i] != 0)
				complete++;
		}
		/*Score manipulation */
		if (sc != 0)
		{
			if (complete == revealed.Length)
			{
				int final_score = (10 - wronged) * 10;
				this.score += final_score;
				total_score += final_score;
				nextmovie();
			}
			updateWordIndicator();
		}
		return ret;
	}

	public void alphabetFuction(string alphabet)
	{
		s = alphabet[0];
		result = true;
	}

	private void updateScoreIndicator()
	{
		scoreIndicator.text = "Score: " + score;
	}

	private void updateWordIndicator()
	{
		string displayed = "";

		/* Build up the display string */
		for (int i = 0; i < revealed.Length; i++)
		{
			char c = revealed[i];
			if (c == 0)
			{
				c = '_';
			}
			displayed += ' ';
			displayed += c;
		}
		wordIndicator.text = displayed;
	}

	private void setWord(string word)
	{
		word = word.ToUpper();
		this.word = word;
		if (word == "")
		{
			revealed = new char[word.Length];
			updateWordIndicator();
			scoreIndicator.text = "";
			TotalScore.text = "Total Score: " + total_score;
			win();
		}
		else
		{
			TotalScore.text = "";
			revealed = new char[word.Length];
			for (int i = 0; i < word.Length; i++)
			{
				char c = word[i];
				if (c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U' || c == '/')
				{
					revealed[i] = c;
				}
			}
			updateWordIndicator();
			updateScoreIndicator();
		}
	}

	public void next()
	{
		bw.reset();
		setWord(Dictionary.instance.next(0));
	}

	public void reset()
	{
		score = 100;
		wronged = 0;
		nexttext.SetActive(false);
		keyboard.SetActive(true);
		restartbutton.SetActive(false);
		nextbutton.SetActive(false);
		wintext.SetActive(false);
		updateScoreIndicator();
		next();
	}

	public void nextmovie()
	{
		nexttext.SetActive(true);
		keyboard.SetActive(false);
		nextbutton.SetActive(true);
	}

	public void nextbuttonclicked()
	{
		reset();
	}

	void win()
	{
		wintext.SetActive(true);
		losetext.SetActive(false);
		keyboard.SetActive(false);
		restartbutton.SetActive(true);
		retrybutton.SetActive(false);
	}

	void lose()
	{
		losetext.SetActive(true);
		keyboard.SetActive(false);
		retrybutton.SetActive(true);
	}

	public void retry()
	{
		score = 100;
		wronged = 0;
		losetext.SetActive(false);
		total_score -= 100;
		keyboard.SetActive(true);
		retrybutton.SetActive(false);
		revealed = new char[word.Length];
		for (int i = 0; i < word.Length; i++)
		{
			char c = word[i];
			if (c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U' || c == '/')
			{
				revealed[i] = c;
			}
			else
			{
				revealed[i] = (char)(0);
			}
		}
		updateScoreIndicator();
		updateWordIndicator();
		bw.reset();
	}

	public void QuitGame()
	{
		Debug.Log("Quit!!!");
		Application.Quit();
	}

	public void Restart()
	{
		Start();
	}
}
