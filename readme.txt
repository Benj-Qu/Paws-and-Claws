Player:
1. gold_spike_scene 里有gameobject player_1 and player_2
2. player must have tag "Player"
3. Player must have box collider2D and rigidbody

Block:
1. Block switch: 选中某个block时要call blockController的SelectBlock(i)才能移动该block
2. Block selection: 玩家抢到某个block时要call blockController的Player1GetBlock/Player2GetBlock来mark
3. 每个block有自己的public block_id需要手动输入
4. block must have box collider and rigidbody
5. All Flying Objects must have a "Floor Block" Component

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
   2. PowerUp 变大，加速（future）， 推进器（future）
   3. possible future: character attack each other
3. map scale (future)
4. more Blocks (future)
5. 选择block可以有简单动画显示block效果 （外援）
6. Make the story more natural (future, 画一点，外援)
   1. 一猫一狗
   2. music
7. win mechanism: flag generation （）
   1. 计分制 积分制获胜条件：每控制flag一段时间得一分 而非最后一刻旗子数量 UI scoreboard
   2. 多来点旗 金币
   3. Football style: each player has their home flag （maybe future）
   4. change flag generation alg （maybe future）
8. 可以加一些guide level (说明是competing还是cooperation）
   1. follow each phase: player control, selection ,placement, flag
   2. 试玩 一个round 的过程， 放block，跳， 抢旗 （互相撞）。

小改进：

1. 放下block之后自动切换到下一个

1. block 放置失败 indicator
2. Grid make smaller
3. indicate 哪个block是自己要放的， 边框
4. indicate block set or not, 勾叉
5. level 1名字confusing
6. block边界 （不同大小block不能一出去）
7. 更多种类的block/地形
8. 只攻击对方/对自己有特殊效果的block （maybe future）
9. 出生点不同（maybe future）



MAR 17

1. tutorial text align to right and flag fix generate (yb). √

2. intro level0 level1 tutorial button, connect tutorial and level 0 xinyi. √

3. pumpkin bug: 1. second round 不会生成2。飞出去不会消失 yb

4. last 10 second double score zhemin

5. second round placement player allow to move bug xinyi

6. Change level name xinyi

7. bomb can overlap hint yb

8. level0 hint text move to ground. xinyi

9. second round block use collectables. zhemin zeyi

10. 狗猫叫 when obtain flag. jyy

11. change background intro jyy

12. placement flag 不能变色。jyy

13. bonus survey. all

14. esc panel stop game (chatgpt) Zeyi panel layer above selection block.






MAR 21
1. 说明goal是flag 解释score (blue / red "+1" beside the flag & “-15”)
3. score不明显 难以注意到 score加粗
4. potion description (polish sprites) (对面拿到旗触发效果）
5. round1/3 2/3 3/3 三局两胜
6. Block的数量变成4，2，2， 2的时间减半
       最后一轮可以考虑全换成道具
8. Combine placement tutorial with level 0, reduce the number of block
10. 字太多了 tutorial里一次出现的内容太多了
11. Change player movement tutorial to one scene
14. 放block的时候indicate不同player 加icon或者颜色
15. 游戏结束忘记把player彻底reset了（）
16. 音效比背景音乐声音大很多 音效太响太惨烈
20. choose block tutorial（换成框选 而不是按了左右就选了 / keyboard sprites on the choices
21. Adjust the size of the blocks
23. 优化collision

New Map:
1. 中国：房檐顶上 / 桌子&花瓶
2. 橄榄球场： 裁判扔橄榄球和红牌
3. 极光：扔冰锥 + 冰块block（滑） 碎冰block

Not now
1. Rotate the block
2. 玩家互相攻击（如果是controller的话可以用右trigger
3. 有点过于像super chicken horse，想一想怎么做的不同
4. powerup可以重复使用
5. 自己放的block只能打别人
6. 显示可以攻击的block的攻击范围


Austin:
1. Integrate tutorial level into the game. Use sprite/scene to show the difficulty level (green -> on fire, storm)
	Change the control to make it more intuitive (e.g., grab blocks by jumping to it, and when the player gets a block, it has bubble around it so that it can place)
2. Remove michigan & ohio logos. Dogs and cats fighting all over the world sounds interesting
3. Change to controllers
4. "You can walljump" instead of "You can walljumping"
5. Overtime when the player falls behind holds the majority flags

Mar 24:
1. larger map camera move.

