using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trail : MonoBehaviour {
    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particleArray;
    Curve[] particle;
    // Use this for initialization
    private int count = 500;
    public float speed;

	void Start () {
        speed = 0.5f;
        particleArray = new ParticleSystem.Particle[count];
        particle = new Curve[count];
        particleSystem.maxParticles = count;
        particleSystem.Emit(count);
        particleSystem.GetParticles(particleArray);
        Init();
	}

    // Update is called once per frame
    void Update()
    {
        if (mod == true)
        {
            int i;
            int level = 2;
            for (i = 0; i < count; i++)
            {
                //外层粒子顺时针旋转
                if (i % level > 0)
                {
                    particle[i].angle -= (i % level + 1) * speed;

                }
                else
                {
                    //内层逆时针旋转
                    particle[i].angle += (i % level + 1) * speed;
                }

                particle[i].angle = (particle[i].angle + 360.0f) % 360.0f;
                particle[i].Draw();
                particleArray[i].position = new Vector3(particle[i].getX(), particle[i].getY(), 0.0f);
            }
        }
        else
        {

            draw3d();
        }

        particleSystem.SetParticles(particleArray, particleArray.Length);
    }

    float begintheta = 0f;
    public bool mod = false;

    void draw3d()
    {
        float theta = begintheta;
        for(int i = 0; i < count; i++)
        {
            particle[i].angle -= speed;
            particle[i].draw_3d(theta);
            theta += 0.5f;
            theta %= 360f;
            particleArray[i].position = new Vector3(particle[i].getX(), particle[i].getY(), 0.0f);
        }
        //begintheta += 2f;
    }

    private void Init()
    {
        int i;
        for (i = 0; i < count; i++)
        {
            float angle = Random.Range(0.0f, 360.0f);
            particle[i] = new Curve(angle);
            //particle[i].Draw();
            //particleArray[i].position = new Vector3(particle[i].getX(), particle[i].getY(), 0f);
        }

        particleSystem.SetParticles(particleArray, particleArray.Length);
    }
}
