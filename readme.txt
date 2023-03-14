Player:
1. gold_spike_scene 里有gameobject player_1 and player_2
2. player must have tag "Player"
3. Player must have box collider2D and rigidbody

Block:
1. Block switch: 选中某个block时要call blockController的SelectBlock(i)才能移动该block
2. Block selection: 玩家抢到某个block时要call blockController的Player1GetBlock/Player2GetBlock来mark
3. 每个block有自己的public block_id需要手动输入
3. block must have box collider and rigidbody

Control:
1. player 1: WASD, left shift for switch block, z for place block on map.
2. player 2: ArrowKey, right shift for switch block, m for palce block on map.
3. During block selection:
    1. Player1: A left choice, d right choice. Options on left side.
    2. Player2: left arrow left choice, right arrow right choice. Options on right side.

Exit Menu:
1. Press ESC will activate the menu

All Blocks:
1. When implementing blocks: Add block image(sprite) to Resources/Sprites folder, and add block_id, sprite name pair into the dict reside in script AllCards.cs.

TODO:
1. Revise the border condition of block position according to different size of the block
2. in gold spike return to menu reenter cannot selection image appear.
3. spike attack in placement.

Future:

1. (future) Amount of each block displayed and applied when player are placing blocks.
2. (future) Instantiate a new block when one block is set if this block amount > 0.
3. (future )Add image of each block (Like Clash Royale).
4. (Future)Add block selection animation: block selected fly to the inventory bar.
5. switch block when one placement is over 
6. fobid switch to the block that are set
7. continuous movement by holding key.

Bugs:
1. Should blocks from the two players collide with each other when they are not set (not put down on the map yet)? not sure.



P3 playtest

Design level 的比较大的：

1. 两个player都能放，别人放的我也能上，所以怎么放不是很有策略性，可以考虑一个player放的对自己优势对别人有负面影响。
   1. 多个round
2. 增加对战性 让玩家可以阻碍对方 
   1. 碰撞
   2. PowerUp 变大，加速，huojian
   3. possible future: character attack each other
3. map scale (future)
4. more Blocks (future)
5. 选择block可以有简单动画显示block效果
6. Make the story more natural (future, 画一点，外援)
   1. 一猫一狗
7. win mechanism: flag generation （）
   1. 计分制 积分制获胜条件：每控制flag一段时间得一分 而非最后一刻旗子数量
   2. 多面旗 powerup 金币
   3. Football style: each player has their home flag
   4. maybe change flag generation alg
8. 选择block需要tutorial 可以加一些guide level (说明是competing还是cooperation）
   1. follow each phase: player control, seleciton ,placement, flag
   2. first basic block, 试玩

小改进：

1. 放下block之后自动切换到下一个

1. 出生点不同 （对称）
2. block 放置失败 indicator
3. 用两个键盘
4. Grid make smaller
5. indicate 哪个block是自己要放的
6. level 1名字confusing
7. block边界 （不同大小block不能一出去）
8. 更多种类的block/地形
9. 只攻击对方/对自己有特殊效果的block （maybe future）





