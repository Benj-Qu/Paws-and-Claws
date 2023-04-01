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
2. effect of the potion (on player), for example a shield for invisible potion.
3. ask player about selection, make it not fly to another player?
4. change the block in the winter land scene, use ice block.

Mar 27:
主要任务：
1. tutorial
	镜头给到旗帜，来表明要拿旗
	tutorial 限制摆的目标位置 + attack
	Tutorial 玩家&旗子的图没改
	player 有颜色的衣服
	tutorial场景改成农场

2. attack 攻击其他player
	knockback， cd

3. controller
	Intro Scene Controller Control
	Controller Key for esc menu page
	block selection 改成框选

6. 对方在flag上时不能抢旗 一直在flag上时判断会有问题（需要出去再进来）

8. Movement Animation
9. 结算动画+transition （爪子伸出来）

12. party time: 最后10秒巨大的倒计时
13. 左上角round， 时间改成数字在右上角 删掉progress bar; 显示领先的人的图片

14. player碰撞 Player碰撞没加controller判断
18. 旗子特判不能生成在前景后面
19. 选block界面蒙版不够深 有时候看不清block和地图 （加一个mask遮住block）
21. 放下block之后框改成白的
22. 猫和狗放block的时候区别不明显

23. block的介绍 框住的block放动画 如果致死放☠️
28. 灯笼重量更大 蒸笼小一点 熊猫换掉一个
29. 背景颜色更深来表明哪些地方可以站
30. 右边出左边进入，左边出右边进，
31. 向下攻击block，制裁抢占高点 （火山喷发）
33. 全局陷阱，参考鸡马海啸
34. 2*1 block
	浮冰 in northland
	缆车 in winter land
	火山 in 火山

不知道怎么复现的bug: (需要测试）
1. winter land 有时有奇怪的collider; 熊猫的collider好像不太对 可能是不会转
2. 跳在其他player头上时有时跳不起来 player movement 跳不起来？
	悬浮？可能竹子卡住？

Small bugs / not now:
1. 连续block有时会卡住
2. 旗子周围可以空一圈？
3. 夺旗的时候更juicy一点 不只是换个颜色 可以搞个动画
4. 多个选项里选block
5. 选择自己的技能
6. 长按加速
7. 单人level（地图难），自定义规则（几轮）
8. block转方向

Mar31
1. background figure 更大让camera不会出边界 改旗子范围
2. bug xinyi
3. 框选放动画zeyi
4. 缆车 yibei
5. intro xinyii controller
6. bug round1 直接结算结束 xinyi
7. 到camera移动结束才开始stage1 zeyi
8. partytime text final round don't disappear zeyi
10.选卡框颜色 选到消失 黄色和粉色xinyi
11.hanyibei push
12. timedisplayer add to other scene. xinyi
13. script yibei
14.








