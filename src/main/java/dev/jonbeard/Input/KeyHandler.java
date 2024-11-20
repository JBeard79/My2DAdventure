package dev.jonbeard.Input;

import dev.jonbeard.GamePanel;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

public class KeyHandler implements KeyListener {
    public boolean upPressed, downPressed, leftPressed, rightPressed, enterPressed;
    GamePanel gp;

    public KeyHandler(GamePanel gp) {
        this.gp = gp;
    }

    @Override
    public void keyTyped(KeyEvent e) {
    }

    @Override
    public void keyPressed(KeyEvent e) {
        int code = e.getKeyCode();

        if (gp.gameState == GamePanel.TITLE_STATE) {
            if (code == KeyEvent.VK_W) {
                gp.userInterface.commandNum--;
                if (gp.userInterface.commandNum < 0) {
                    gp.userInterface.commandNum = 2;
                }
            }
            if (code == KeyEvent.VK_S) {
                gp.userInterface.commandNum++;
                if (gp.userInterface.commandNum > 2) {
                    gp.userInterface.commandNum = 0;
                }
            }
            if (code == KeyEvent.VK_ENTER) {
                switch (gp.userInterface.commandNum) {
                    case 0: {
                        gp.gameState = GamePanel.PLAY_STATE;
                        gp.playMusic(0);
                    }
                    break;
                    case 1: {
                        //add later
                        break;
                    }
                    case 2: {
                        System.exit(0);
                    }
                    break;
                    default:
                        break;
                }
            }
        }

        if (gp.gameState == GamePanel.PLAY_STATE) {
            if (code == KeyEvent.VK_W) {
                upPressed = true;
            }
            if (code == KeyEvent.VK_S) {
                downPressed = true;
            }
            if (code == KeyEvent.VK_A) {
                leftPressed = true;
            }
            if (code == KeyEvent.VK_D) {
                rightPressed = true;
            }
            if (code == KeyEvent.VK_P) {
                gp.gameState = GamePanel.PAUSE_STATE;
            }
            if (code == KeyEvent.VK_ENTER) {
                enterPressed = true;
            }
        }

        // PAUSE STATE
        else if (gp.gameState == GamePanel.PAUSE_STATE) {
            if (code == KeyEvent.VK_P) {
                gp.gameState = GamePanel.PLAY_STATE;
            }
        } else if (gp.gameState == gp.dialogState && code == KeyEvent.VK_ENTER) {
            gp.gameState = GamePanel.PLAY_STATE;
        }
    }

    @Override
    public void keyReleased(KeyEvent e) {
        int code = e.getKeyCode();
        if (code == KeyEvent.VK_W) {
            upPressed = false;
        }
        if (code == KeyEvent.VK_S) {
            downPressed = false;
        }
        if (code == KeyEvent.VK_A) {
            leftPressed = false;
        }
        if (code == KeyEvent.VK_D) {
            rightPressed = false;
        }

    }
}
