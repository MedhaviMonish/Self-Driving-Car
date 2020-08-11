# Self Driving Car


This is an implementation of self driving car using Reinforcement learning algorithm called Advantage Actor Critic.
There are 2 continuous actions and inputs are Car location , goal location , car speed and a raycast that checks for obstacle in 3 directions.

This is a 1-step A2C.
The car and track is created in Blender and the simulation is created in Unity. [Here's the link for Unity ml-agents](https://github.com/Unity-Technologies/ml-agents)

The jupyter notebook file is my implementation of A2C.

The implementation of RL algorithms provided by ml-agents can easily work with multiple agents,
but there are issues with training our implementations with mutiple agents.
That's why you should use the scene "Race track.unity" for training with inbuilt algorithms
and "ForOwnImplementatiom.unity" for training with your implementation.


All the prefabs, scripts and scenes required to build the game has been provided.
You will find a CARAI.nn file this is a brain that drives the car, this was created using  ml-agent's implementation of PPO.

## Tools and libraries
Unity Version: Unity 2019.3.0f6

Blender: 2.8

ML-Agents version: 0.19.0.dev0

TensorFlow version: 2.3.0
