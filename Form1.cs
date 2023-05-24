//-----------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------
//Description:
//A game where you control a character who is trying to defend his hometown from the oncoming 
//horde of zombies. The town is to the left of the screen. They player has gone out to hold them back.
//if the player is hit by the zombies or they get past you to the left of the screen then you will lose health.
//Survivors will appear and run around randomly. You must move over them to rescure them and get more
//points. Killing zombies will get you points for a higher score. When the boss zombie spawns the other zombies
//speed up.Killing the boss will yield more points.
//------------------------------------------------------------------------------------------------------------------
//Know bugs/issues
//Sometimes at the start of a new game one of the zombies will spawn below the screen 
//Possible that zombies may sometimes appear on top of one another


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Media;
using WMPLib;

namespace JointProject1  
{
    public partial class Form1 : Form
    {  //All instance variables
        Random randomNum = new Random();

        Image zombie1, zombie2, zombieLeft;
        Image boss, bossLeft, bossRight, bossUp, bossDown; 
        Image survivor, survivorLeft, survivorRight, survivorUp, survivorDown; 
        Image player, playerLeft, playerRight, playerUp, playerDown;
        Image playerLives, threeLives, twoLives, oneLife;
        Image bullet, bulletUp, bulletDown, bulletRight, bulletLeft;
        Image scoreboard = Image.FromFile("Images/scoreboard.png");

        int survivorRespawnTimer, bossSpawnTimer, changeSurvivorDirTimer; 
        int zombiesKilled, survivorsSaved, score; 
        int maxHealth, currentHealth, lives, maxLives; 
        int maxBossHealth, bossCurrentHealth; 
        int playerX, playerY, playerSpeed, playerDir; 
        int zombie1X, zombie1Y, zombie1Width, zombie1Height, zombie2X, zombie2Y, zombie2Width, zombie2Height, zombieSpeed, zombieDamage, bossDamage;
        int bossX, bossY, bossSpeed;
        int bulletXPos, bulletYPos, bulletXMove, bulletYMove, bulletDir, reloadTimer; 
        int survivorX, survivorY, survivorSpeed, survivorDir;

        private static SoundPlayer gunShot = new SoundPlayer("Sounds/gunshot.wav");
        private static SoundPlayer playerDeath1 = new SoundPlayer("Sounds/death1.wav");
        private static SoundPlayer playerDeath2 = new SoundPlayer("Sounds/death2.wav");
        private static SoundPlayer playerOuch1 = new SoundPlayer("Sounds/ouch1.wav");
        private static SoundPlayer playerOuch2 = new SoundPlayer("Sounds/ouch2.wav");
        private static WindowsMediaPlayer backgroundMusic;  
        

        float healthBarLength, bossHealthBarLength;
        bool zombie1Alive, zombie2Alive, bossAlive, bulletAlive, survivorAlive;
        const byte North = 1, South = 2, West = 3, East = 4;

        public Form1()
        {
            InitializeComponent();
            SetUpGame();
            SetUpBackgroundMusic();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private static void SetUpBackgroundMusic()
        //Sets up background music
        {
            FileStream inputFileName = new FileStream("sounds/background.mp3", FileMode.Open);

            backgroundMusic = new WindowsMediaPlayer();
            backgroundMusic.URL = inputFileName.Name;
            backgroundMusic.uiMode = "none";
            backgroundMusic.settings.volume = 50;
            backgroundMusic.settings.setMode("loop", true);
            backgroundMusic.controls.play();
        }

        private void SetUpGame()
        {
            LoadImages();
            InitializeValues();

            // Sets starting images for zombies, boss, survivor, bullet, and the player
            boss = bossLeft;
            zombie1 = zombieLeft;
            zombie2 = zombieLeft;
            survivor = survivorLeft;
            player = playerRight;
            playerLives = threeLives;
            bullet = bulletDown;

            zombieDamage = 40;
            bossDamage = 100;

            zombie1Alive = true;
            zombie2Alive = true;
            bossAlive = false;
            survivorAlive = true;
            bulletAlive = false;
            playerX = 0;
            playerY = 120;

            bossX = this.ClientRectangle.Width + boss.Width;
            bossY = randomNum.Next(60, this.ClientRectangle.Height - boss.Height);

            survivorX = this.ClientRectangle.Width + 100;
            survivorY = randomNum.Next(60, this.ClientRectangle.Height - 100);

            zombie1X = this.ClientRectangle.Width + zombie1Width * 3;
            zombie1Y = this.ClientRectangle.Height / 2;

            zombie2X = this.ClientRectangle.Width + zombie2Width;
            zombie2Y = this.ClientRectangle.Height / 2 + 100;
        }

        private void LoadImages()
        {
            // Gets player images
            playerUp = Image.FromFile("Images/playerUp.png");
            playerDown = Image.FromFile("Images/playerDown.png");
            playerRight = Image.FromFile("Images/playerRight.png");
            playerLeft = Image.FromFile("Images/playerLeft.png");

            // Get player lives images
            threeLives = Image.FromFile("Images/3lives.png");
            twoLives = Image.FromFile("Images/2lives.png");
            oneLife = Image.FromFile("Images/1life.png");

            // Image to be used for zombie1 and zombie2
            zombieLeft = Image.FromFile("Images/zombieLeft.png");

            // Images used to display the zombie boss
            bossLeft = Image.FromFile("Images/bossLeft.png");
            bossRight = Image.FromFile("Images/bossRight.png");
            bossUp = Image.FromFile("Images/bossUp.png");
            bossDown = Image.FromFile("Images/bossDown.png");

            // Images to display the survivor
            survivorUp = Image.FromFile("Images/survivorUp.png");
            survivorDown = Image.FromFile("Images/survivorDown.png");
            survivorLeft = Image.FromFile("Images/survivorLeft.png");
            survivorRight = Image.FromFile("Images/survivorRight.png");

            // Images to display the bullet
            bulletUp = Image.FromFile("Images/bulletUp.png");
            bulletDown = Image.FromFile("Images/bulletDown.png");
            bulletRight = Image.FromFile("Images/bulletRight.png");
            bulletLeft = Image.FromFile("Images/bulletLeft.png");
        }

        

        private void InitializeValues()
        {
            survivorRespawnTimer = 0;
            changeSurvivorDirTimer = 0;
            bossSpawnTimer = 0;
            reloadTimer = 0;

            playerSpeed = 6;
            zombieSpeed = 4;
            survivorSpeed = 6;
            bossSpeed = 1;
            bulletXMove = 15;
            bulletYMove = 15;

            score = 0;
            survivorsSaved = 0;
            zombiesKilled = 0;

            lblSurvivorsSaved.Text = "Survivors Saved: " + survivorsSaved.ToString();
            lblScore.Text = "Score: " + score.ToString();
            lblZombiesKilled.Text = "Zombies Killed: " + zombiesKilled.ToString();

            bossHealthBarLength = 50;
            maxBossHealth = 100;
            bossCurrentHealth = maxBossHealth;

            healthBarLength = 250;
            maxHealth = 100;
            currentHealth = maxHealth;
            maxLives = 3;
            lives = maxLives;

            playerDir = East;
            bulletDir = North;
            survivorDir = West;

            zombie1Width = 36;
            zombie1Height = 36;
            zombie2Width = 36;
            zombie2Height = 36;

            zombieDamage = 40;
            bossDamage = 100;

            zombie1Alive = true;
            zombie2Alive = true;
            bossAlive = false;
            survivorAlive = true;
            bulletAlive = false;
        }



        public void UpdateWorld()
        //Contains all method calls that will change and update the game
        {
            CheckCollisions();
            SurvivorMove();
            BulletMovement();
            bossMove();
            ZombiesMove();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        //Displays everything on the screen except the background image.
        {
            Graphics paper = e.Graphics; //Sets up graphics object
            SolidBrush greenHealthBar = new SolidBrush(Color.Green);
            SolidBrush redHealthBar = new SolidBrush(Color.Red);
            Pen blackPen = new Pen(Color.Black);

            paper.DrawImage(scoreboard, 0, 0); //Draws the silver scoreboard at the top that will contain game related information for the player to see
           
            //Draw player's health bar
            paper.FillRectangle(redHealthBar, 17, 16, 250, 25);
            paper.FillRectangle(greenHealthBar, 17, 16, healthBarLength, 25);
            paper.DrawRectangle(blackPen, 16, 15, 250, 26);

            //Draw player and their lives lives
            paper.DrawImage(player, playerX, playerY, player.Width, player.Height);
            paper.DrawImage(playerLives, 297, 10);

            if (zombie1Alive) //Only draws if the zombie1 is alive
                paper.DrawImage(zombie1, zombie1X, zombie1Y, zombie1Width, zombie1Height);
            if (zombie2Alive)
                paper.DrawImage(zombie2, zombie2X, zombie2Y, zombie2Width, zombie2Height);
            if (bulletAlive)
                paper.DrawImage(bullet, bulletXPos, bulletYPos);
            if (survivorAlive)
                paper.DrawImage(survivor, survivorX, survivorY);
            if (bossAlive)
            {
                paper.DrawImage(boss, bossX, bossY); //Draws the boss
                //Draws the bosses' healthbar relative to the positon the boss is at
                paper.FillRectangle(redHealthBar, bossX - 15, bossY - 12, 50, 8);
                paper.FillRectangle(greenHealthBar, bossX - 15, bossY - 12, bossHealthBarLength, 8);
                paper.DrawRectangle(blackPen, bossX - 16, bossY - 13, 50, 9);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        //Controls what happens when pressing certain keys
        {
            if (e.KeyCode == Keys.W)   //If the W key is pressed
            {
                if (playerY > 60) //Will only move the player if they are not out of the playable part of the top of the screen
                {                   //Otherwise they will not move. This prevents the player from leaving the playable area
                    player = playerUp;  //Change to player sprite to make it face the way it is moving
                    playerY -= playerSpeed; //Move the player in a direction depending on what key was pressed
                    playerDir = North;    //Set playerDir to the direction the player is moving
                }
            }
            else if (e.KeyCode == Keys.S)
            {
                if (playerY + player.Height < this.ClientRectangle.Height)   
                {
                    player = playerDown;
                    playerY += playerSpeed;
                    playerDir = South;
                }
            }
            if (e.KeyCode == Keys.A)
            {
                if (playerX > 0)
                {
                    player = playerLeft;
                    playerX -= playerSpeed;
                    playerDir = West;
                }
            }
            else if (e.KeyCode == Keys.D)
            {
                if (playerX + player.Width < this.ClientRectangle.Width)
                {
                    player = playerRight;
                    playerX += playerSpeed;
                    playerDir = East;
                }
            }

            if (e.KeyCode == Keys.Space) //Space to fire bullet
            {
                if (reloadTimer > 10) //Can only fire bullet every time this gets above 100
                {
                    reloadTimer = 0; //Reset reload timer
                    GetBulletPosition();
                    bulletAlive = true;
                    gunShot.Play();
                }
            }
        }

        private void GetBulletPosition()
        //Gets starting position for the bullet
        {
            if (bulletAlive == false)
            { 
                if (playerDir == North) //If the player is facing or moving in this direction
                {
                    bulletDir = North;   //The bullet direction will be set to this direction also. 
                    bulletYPos = playerY; //Sets starting position of the bullet releative to the direction the player is facing
                    bulletXPos = playerX + (player.Width / 2) - 5;
                }
                else if (playerDir == South)
                {
                    bulletDir = South;
                    bulletYPos = playerY + 20;
                    bulletXPos = playerX + (player.Width / 2) + 5;
                }
                else if (playerDir == East)
                {
                    bulletDir = East;
                    bulletYPos = playerY + (player.Height / 2) - 5;
                    bulletXPos = playerX + player.Width;
                }
                else if (playerDir == West)
                {
                    bulletDir = West;
                    bulletYPos = playerY + (player.Height / 2) - 5;
                    bulletXPos = playerX;
                }
            } //end if
        }


        private void BulletMovement()
        //Controls the movement of the bullet and what happens when bulletAlive is false. 
        {
            reloadTimer++;
            if (bulletXPos < 0 || bulletXPos > this.ClientRectangle.Width || bulletYPos < 65 || bulletYPos > this.ClientRectangle.Height)
            //Check if bullet is off the screen
            {
                bulletAlive = false; //Stop drawing bullet once it leaves the screen
            }
            else if (bulletAlive)
            {//Changes direction of bullet depending on its direction in "bulletDir"
                if (bulletDir == North)  
                {
                    bullet = bulletUp;   //Changes the sprite to relative to the direction the bullet is moving
                    bulletYPos -= bulletYMove; //Moves the bullet in the direction it should be moving in
                }
                else if (bulletDir == South)
                {
                    bullet = bulletDown;
                    bulletYPos += bulletYMove;
                }
                else if (bulletDir == East)
                {
                    bullet = bulletRight;
                    bulletXPos += bulletXMove;
                }
                else if (bulletDir == West)
                {
                    bullet = bulletLeft;
                    bulletXPos -= bulletXMove;
                }
            } //end else if
        } //end method

        private void SurvivorMove()
        //Controls the movements of the survivor when it is alive and what happens when it is no longer alive
        {
            changeSurvivorDirTimer++; 
            survivorRespawnTimer++;

            if (survivorRespawnTimer >= 200)  //Around every 6 seconds the survivor will be capable of re-appearing
            {
                survivorRespawnTimer = 0;  //Reset the timer so after another amount of time it will be able to re-appear again
                survivorAlive = true;
            }

            if (survivorAlive == false)
            {
                survivorX = this.ClientRectangle.Width + survivor.Width;  //Sets new starting positions for the survivor
                survivorY = randomNum.Next(60, this.ClientRectangle.Height - survivor.Height);
            }
            else if (survivorAlive)
            {
                if (bossAlive)
                    survivorSpeed = 8;  //Increases speed when the boss zombie is alive
                else
                    survivorSpeed = 6;

                if (changeSurvivorDirTimer >= 100)  
                { //Will set a new direction for the survivor to walk in
                    if (survivorDir == North || survivorDir == South)    //If the survivor is already walking up and down it will now only go left or right
                        survivorDir = randomNum.Next(3, 5);
                    else if (survivorDir == West || survivorDir == East)//similarly if the survivor is already walking left or right it will now only go up or down
                        survivorDir = randomNum.Next(1, 3); 
                    changeSurvivorDirTimer = 0;     //Reset the timer until the survivor will change direction again.
                }
                if (survivorY < 60)  //Onces the player reaches the edges of the screen it will reverse its direction
                    survivorDir = South;
                else if (survivorY + survivor.Height > this.ClientRectangle.Height)
                    survivorDir = North;
                else if (survivorX < 0)
                    survivorDir = East;
                else if (survivorX + survivor.Width > this.ClientRectangle.Width)
                    survivorDir = West;

                if (survivorDir == North)
                {
                    survivor = survivorUp; //Changes sprite so it faces in the direction it is moving
                    survivorY -= survivorSpeed;  //Changes direction so it will now move up the screen
                }
                else if (survivorDir == South)
                {
                    survivor = survivorDown;
                    survivorY += survivorSpeed;
                }
                else if (survivorDir == West)
                {
                    survivor = survivorLeft;
                    survivorX -= survivorSpeed;
                }

                else if (survivorDir == East)
                {
                    survivor = survivorRight;
                    survivorX += survivorSpeed;
                }
            } //end else if
        }//end method

        private void ZombiesMove()
        //Controls the movements of zombie1 and zombie2 when they are alive and what happens when they are dead.
        {
            if (bossAlive)
                zombieSpeed = 6; //While the boss zombie is alive then the speed is higher
            else
                zombieSpeed = 4;

            if (zombie1X < 0 - zombie1Width)
            {
                zombie1Alive = false;       //Change zombie1Alive to false so it is no longer drawn when it isn't on the screen
                DecreasePlayerHealth(zombieDamage / 2);   //Decrease player health by half of the normal zombie damage if the zombies reaches the left of the screen
            }
            if (zombie2X < 0 - zombie2Width)
            {
                zombie2Alive = false;
                DecreasePlayerHealth(zombieDamage / 2);
            }
            if (zombie1Alive == false)
            {   //If zombie1 is dead the its x and y position are reset
                zombie1X = this.ClientRectangle.Width + zombie1Width;
                zombie1Y = randomNum.Next(60, this.ClientRectangle.Height - zombie1Height);
                if (randomNum.Next(1, 51) == 1) //  1/50 chance every 20 milliseconds ( around every second) after the zombie has been killed it will spawn back
                    zombie1Alive = true;       
            }
            if (zombie2Alive == false)
            { //If zombie2 is dead the its x and y position are reset
                zombie2X = this.ClientRectangle.Width + zombie2Width;
                zombie2Y = randomNum.Next(60, this.ClientRectangle.Height - zombie2Height);
                if (randomNum.Next(1, 51) == 1)
                    zombie2Alive = true;
            }
            zombie1X -= zombieSpeed; //Both of these zombies only move left
            zombie2X -= zombieSpeed;
        }

        private void bossMove()
        //Controls the way the boss zombie moves if it is alive or resets its position if its dead.
        {
            bossSpawnTimer++;

            if (bossSpawnTimer >= 400) //When this timer gets to 400 then the boss zombie will be able to spawn
            {
                bossAlive = true;
            }

            if (bossAlive == false)
            {  
                bossX = this.ClientRectangle.Width + boss.Width; //Reset the positon of the boss zombie
                bossY = randomNum.Next(60, this.ClientRectangle.Height - boss.Height);
            }
            else if (bossAlive)
            {
                bossSpawnTimer = 0; //Timer is 0 until the boss is killed, then it will start incrementing
                if (bossX < playerX)
                {
                    boss = bossRight; //Change boss sprite to make it face in the direction it is moving
                    bossX += bossSpeed;  //Change the direction the boss zombie is moving in
                }
                else if (bossX > playerX)
                {
                    boss = bossLeft;
                    bossX -= bossSpeed;
                }
                if (bossY < playerY)
                {
                    boss = bossDown;
                    bossY += bossSpeed;
                }
                else if (bossY > playerY)
                {
                    boss = bossUp;
                    bossY -= bossSpeed;
                }
            } //end else if
        } // end method

        private bool DetectCollision(int object1X, int object1Y, int object1Width, int object1Height, int object2X, int object2Y, int object2Width, int object2Height)
        //Checks if there is no collision between two objects, if there is then return false or if there is a collision, return true.
        {
            if (object1X + object1Width <= object2X || object1X >= object2X + object2Width ||
                object1Y + object1Height <= object2Y || object1Y >= object2Y + object2Height)
                return false;    //No collision
            else
                return true; //Collision
        }

        private void CheckCollisions()
        //Checks if objects have collided using the DetectCollision method and if they have then actions are carried out on them objects or values are changed (score)
        {
            bool collisionPlayerZombie1 = DetectCollision(playerX, playerY, player.Width, player.Height, zombie1X, zombie1Y, zombie1Width, zombie1Height);
            bool collisionPlayerZombie2 = DetectCollision(playerX, playerY, player.Width, player.Height, zombie2X, zombie2Y, zombie2Width, zombie2Height);
            bool collisionBulletZombie1 = DetectCollision(bulletXPos, bulletYPos, bullet.Height, bullet.Width, zombie1X, zombie1Y, zombie1Width, zombie1Height);
            bool collisionBulletZombie2 = DetectCollision(bulletXPos, bulletYPos, bullet.Height, bullet.Width, zombie2X, zombie2Y, zombie2Width, zombie2Height);
            bool collisionBulletSurvivor = DetectCollision(bulletXPos, bulletYPos, bullet.Height, bullet.Width, survivorX, survivorY, survivor.Width, survivor.Height);
            bool collisionPlayerSurvivor = DetectCollision(playerX, playerY, player.Width, player.Height, survivorX, survivorY, survivor.Width, survivor.Height);
            bool collisionPlayerBoss = DetectCollision(playerX, playerY, player.Width, player.Height, bossX, bossY, boss.Width, boss.Height);
            bool collisionBulletBoss = DetectCollision(bulletXPos, bulletYPos, bullet.Height, bullet.Width, bossX, bossY, boss.Width, boss.Height);

            if (collisionPlayerZombie1)
            {
                zombie1Alive = false; 
                DecreasePlayerHealth(zombieDamage);
                ResetPlayerPosition();
            }
            else if (collisionPlayerZombie2)
            {
                zombie2Alive = false;
                DecreasePlayerHealth(zombieDamage);
                ResetPlayerPosition();
            }
            else if (collisionBulletZombie1)
            {
                zombie1Alive = false;
                bulletAlive = false;
                bulletXPos = 0;  //This removes the bullet from the area were zombies could still hit off it and die despite it not being displayed
                bulletYPos = 0;
                zombiesKilled++;
                ChangeScore(10);  //Increases the score by 10 to award the player for killing the zombie
                lblZombiesKilled.Text = "Zombies Killed: " + zombiesKilled.ToString(); //Display zombies killed to the user
            }
            else if (collisionBulletZombie2)
            {
                zombie2Alive = false;
                bulletAlive = false;
                bulletXPos = 0;
                bulletYPos = 0;
                zombiesKilled++;
                ChangeScore(10);
                lblZombiesKilled.Text = "Zombies Killed: " + zombiesKilled.ToString();
            }
            else if (collisionPlayerSurvivor)
            {
                survivorAlive = false; //Survivor wont be drawn until this becomes true again
                survivorsSaved++;
                ChangeScore(50); //Adds 50 to score for saving the survivor
                lblSurvivorsSaved.Text = "Survivors Saved: " + survivorsSaved.ToString(); 
            }
            else if (collisionBulletSurvivor)
            {
                survivorAlive = false;
                bulletAlive = false;
                bulletXPos = 0;
                bulletYPos = 0;
                ChangeScore(-50);
            }
            else if (collisionPlayerBoss)
            {
                DecreasePlayerHealth(bossDamage);
                ResetPlayerPosition();
            }
            else if (collisionBulletBoss)
            {
                DecreaseBossHealth(5);
                bulletAlive = false;
                bulletXPos = 0;
                bulletYPos = 0;
            }
        }

        private void ChangeScore(int scoreAmount)
        //Increases score by the amount passed to it
        {
            score += scoreAmount;
            lblScore.Text = "Score: " + Convert.ToString(score);
        }

        private void ResetPlayerPosition()
        //Resets players position to the left of the screen
        {
            int ouchSound = randomNum.Next(0, 2); 
            if (ouchSound == 0)  //If the random number is 0 then play this the first "ouch" sound
                playerOuch1.Play();
            else
                playerOuch2.Play(); //else play the second
            player = playerRight;
            playerX = 0;
            playerY = randomNum.Next(60, this.ClientRectangle.Height - player.Height);
        }

        private void DecreasePlayerHealth(int damage)
        //To decrease the players health by the amount passed to it and may decrease lives. Check if they have been killed and restart or exit.
        {
            if (currentHealth - damage > 0) //If the damage dealt will not kill the player by putting their health 0 or lower 
            {
                currentHealth -= damage;
                healthBarLength = (250) * ((float)currentHealth / maxHealth); //Get a percentage of the players remaining health and multiply this by the
                                                                             //length of the healthbar to make the green rectangle representing health smaller
                                                                       //e.g if currentHealth/maxHealth is 20/100. Then 250 * 0.2 is 50. The green rectangle is now 50 
            }
            else
            {
                currentHealth = 100;   //Reset health to 100
                healthBarLength = (250) * ((float)currentHealth / maxHealth); //Display health by making the green rectangle back to its original size of 250( 250 * (100/100)) = (250 * 1)
                lives--;
                ResetPlayerPosition();
                if (lives == 2)   //Replaces lives image with how many lives the player has left
                    playerLives = twoLives;
                else if (lives == 1)
                    playerLives = oneLife;
                else if (lives == 0)
                {
                    int deathSound = randomNum.Next(0, 2);
                    if (deathSound == 0)
                        playerDeath1.Play();
                    else
                        playerDeath2.Play();
                    //Show scoreboard
                    if (MessageBox.Show("Score: "+score+"\n"+ "Zombies Killed: "+zombiesKilled+"\n"+ "Survivors Saved: "+survivorsSaved+"\n"+ "Do you want to restart the game?", 
                        "You died! Restart game?", MessageBoxButtons.YesNo) == DialogResult.Yes)  //Learned this from here: http://stackoverflow.com/questions/3036829/how-to-create-a-messagebox-with-yes-no-choices-and-a-dialogresult-in-c
                    {
                        SetUpGame();   //Set up all variables again to initial values to start a fresh game
                    }
                    else
                    {
                        Application.Exit();
                    }

                }   //end else if
            } //end else
        }
        private void DecreaseBossHealth(int damage)
        //To decrease the bosses health and check if it has been killed.
        {
            if (bossCurrentHealth - damage > 0) //If the result of this is 0 or less then that means the boss has been killed
            {
                bossCurrentHealth -= damage; 
                bossHealthBarLength = (50) * ((float)bossCurrentHealth / maxBossHealth);  //Get the percentage of health left and multiply this by the length of 
                                                                                             //of the health bar to redraw the green rectangle at a smaller length
            }
            else  //The boss has been killed
            {
                bossCurrentHealth = 100;  //Reset boss health
                bossHealthBarLength = (50) * ((float)bossCurrentHealth / maxBossHealth);
                bossAlive = false;
                zombiesKilled++;
                ChangeScore(50);
            }
        } // end method
    } //end class
}
