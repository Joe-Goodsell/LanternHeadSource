[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/CibnTZFQ)

# Project 2 Report

Read the [project 2
specification](https://github.com/COMP30019/Project-2-Specification) for
details on what needs to be covered here. You may modify this template as you see fit, but please
keep the same general structure and headings.

Remember that you must also continue to maintain the Game Design Document (GDD)
in the `GDD.md` file (as discussed in the specification). We've provided a
placeholder for it [here](GDD.md).

## Table of Contents

* [Evaluation Plan](#evaluation-plan)
* [Evaluation Report](#evaluation-report)
* [Shaders and Special Effects](#shaders-and-special-effects)
* [Summary of Contributions](#summary-of-contributions)
* [References and External Resources](#references-and-external-resources)


## Evaluation Plan

### Evaluation Goals

- Can users survive for 5 minutes? (is it too hard)
- High usability and fun scores
- Are the controls and mechanics intuitive for the average player?

### Evaluation Techniques

- Quantitative: Usability questionnaires, Gamification questionnaire
- Qualitative: Think Aloud, Open-text questions

#### Think-Alound

- There will be two evaluation dates: one at an early stage with 2-3 users and a subsequent one after the questionnaire evaluation with an additional 2-3 participants.
- Each group member conducts this method with at least one person.
- The user plays the game and will be asked to comment on the game at the end:
    - Positive/negative/general comments.
- In total, 5 users evaluate the game, making notes either digitally or with paper and pencil.
- The evaluation can be conducted either in person or online.
- Audio and screen will be recorded during the evaluation.
- All test participants will receive the same initial information through a specially created [introduction sheet](), ensuring comparability across sessions, even when conducted by different test leaders.

Why this method:

- by vocalising thoughts, reactions, and decision making the user gives great insights which will help us understand the cognitive processes and offer valuable feedback for game improvements
- We have chosen for an early implementation of the think-aloud method, as this allows us to get first impressions of unexpected inputs and ways of thinking. Furthermore we will be able to fix major bugs at an early stage if necessary.
- We chose a smaller number of test subjects because of the greater time required to carry out and evaluate the study.


#### Questionnaire

- Each group member finds 4-5 people to evaluate our game
- The user plays the game and answers a questionnaire
- In total 16-20 users that evaluate our game
- Feedback can be provided in an open text box
- All test persons receive the same initial information in the form of a specially created [introduction sheet]()

Why this method:

- Questionnaires provide a consistent set of questions for all participants, ensuring that the feedback is standardised. This allows for easier analysis and comparison of results.
- With structured responses, such as Likert scales, the data collected can be easily quantified, making it simpler to identify trends, preferences, and areas of improvement.
- Questionnaires can be distributed to a large number of participants simultaneously. This allows for the collection of a substantial amount of feedback in a relatively short period.

Evaluation consists of two questionnaire parts

- [UEQ](https://www.ueq-online.org) & [UEQ+](https://ueqplus.ueq-research.org) focus on the usability of the game
    - User Experience Questionnaire evaluates
        - Efficiency -> how good does the user understand what to do
        - Satisfaction -> is the game easy to play?
        - Usability
    - Why and explanation UEQ and UEQ+
        - Standardised UEQ questionnaire for measuring the user experience of interactive products.
        - UEQ contains 26 items that can be classified into six categories: Attractiveness, Perspicuity, Efficiency, Dependability, Stimulation and Novelty.
        - The UEQ+ is an extension of the UEQ Questionnaire, which is modular so that it can be optimally adapted to the evaluation of a specific product
    - [GUESS-18](https://uxpajournal.org/validation-game-user-experience-satisfaction-scale-guess/) assesses the enjoyment and playability of the game
        - Game User Experience Satisfaction Scale evaluates
            - Usability/Playability               	
            - Narratives          	
            - Play Engrossment      	
            - Enjoyment     	
            - Creative Freedom 
            - Audio Aesthetics 
            - Personal Gratification 
            - Social Connectivity 
            - Visual Aesthetics 
        - about GUESS
            - Abridged version of the [GUESS](https://soar.wichita.edu/bitstream/handle/10057/11604/d15018_PHAN_Mikki_SP15%20SEQUESTER.pdf;jsessionid=35F354DE576A784F3DAFB6B59BE24519?sequence=1) (55 items), which is used to evaluate video games
            - Based on 13 game related questionnaires (measuring motivation, engagement, enjoyment, experience, usability, playability,...)
            - was created through the process of Item pool creation, followed by an expert review, a pilot study and a final expert review of the statements
            - The participants are presented with short statements that are recorded on a 7-point Likert scale from "strongly disagree" to "strongly agree".
        - why GUESS
            - 7-point scale
                - This makes it easier for participants to rate, as many tend to avoid the extreme ranges, and at the same time another scale reduces the jumps in meaning between two values.
            - short statements with good comprehensibility
                - therefore the subjective impression can be recorded briefly and clearly
            - Randomised order of question
                - Avoidance of sequence effects
            - Social connectivity is not surveyed in the context of our game. In addition, usability/playability is already covered by the UEQ, which is why the evaluation of this questionnaire will not focus on it here. However, it is a good opportunity to compare whether similar results emerge from both surveys. 


### Evaluation Process

1. Introduction before the game (Introduction Sheet)
    - This informs the participants about the evaluation process and their legal options. At the same time, an introduction sheet offers the possibility to provide the players with information about the task to be carried out as well as instructions on how to participate in the evaluation. 
2. User plays the game (Think-Aloud: with verbalizations)
3. Fill in questionnaire / Commenting on the game (Think-Aloud)

### Participants

- Think-Aloud: Each group member recruits at least one person (friends & family) - in total 5 participants will be recruited for this method
- Questionnaires: Each group member recruits 4-5 people (friends, family, uni)

What qualifying criteria will you use to ensure that they are representative of your target audience?

- Age: 18+ (open end, so you’re of legal age and allowed to participate in survey)
- Interested in gaming, survival games, video games
- English speaking
- No prior information about the game (only given information of introduction sheet)
- For questionnaire evaluation (person has not played the game in the process of the Think Aloud method)

All participants receive the same information exclusively through the Introduction Sheet in advance to ensure that the players are not influenced or receive different amounts of information in advance due to different experiment leaders conducting the experiment.

### Data Collection

What sort of data is being collected?
- Quantitative data
    - responses from questionnaires
    - metrics such as completion rates
- Qualitative data
    - feedback from Think-Aloud method
    - open-text responses

How will you collect the data?
- Think-Aloud
- Questionnaires

What tools will you use?
- Think-Aloud: Take notes in digital form or with pen and paper, audio and screen recording
- Questionnaire: Google Forms


### Data Analysis

How will you analyse the data?
- Data analysis tools for Excel are used to evaluate the evaluation. 
- For the evaluation of the UEQ and the UEQ+ Questionnaire, the respective data analysis tool by Dr. Martin Schrepp is used. (Link Excel Sheets)
- For the evaluation of the GUESS-18, the data analysis tool by William J. Shelstad is used. (Link Excel sheet)

<br>

- Make graphics for report and evaluation of our goals
- Draw conclusions on usability improvements
- Draw conclusions on game-fun
- Create work plan to solve identified issues
- If one user makes a comment, do you act on it? Do you need multiple people to have commented on the same thing?
    - we will decide on each issue independently 
    - if only one person points out a major problem or something we consider as very important, we will definitely make changes
    - smaller, not game relevant points have to be discussed in the group whether they should be changed or not (also if more than one person mentioned the minor issue)


### Responsibilities

Who is responsible for each task?
- All team members are responsible for recruiting participants for the evaluation. 
- Creating evaluation plan - Linda
- Evaluation analysis - Linda &
- Implementing changes - everyone

How will you ensure that everyone contributes equally?
- Most of the things are done by everyone therefore it is contributed equally already
- Other tasks that are not contributed to in equal measure will balance out through implementation

### Timeline

<p align="center">
    <img src="Images/Timeline_Evaluation" width="200">
</p>
<p align="center">
    <img src="Images/Gantt_Final_GDD_legend.png" width="200">
</p>

## Evaluation Report

TODO (due milestone 3) - see specification for details

## Shaders and Special Effects

TODO (due milestone 3) - see specification for details

## Summary of Contributions

TODO (due milestone 3) - see specification for details

## References and External Resources

TODO (to be continuously updated) - see specification for details
