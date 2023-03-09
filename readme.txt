Players & flags & blocks z position is -1

Player:
1. gold_spike_scene 里有gameobject player_1 and player_2
2. player must have tag "Player"
3. Player must have box collider and rigidbody

Block:
1. Block switch: 选中某个block时要call blockController的SelectBlock(i)才能移动该block
2. Block selection: 玩家抢到某个block时要call blockController的Player1GetBlock/Player2GetBlock来mark
3. 每个block有自己的public block_id需要手动输入
3. block must have box collider and rigidbody

TODO:
1. Revise the border condition of block position according to different size of the block
2. Combine two scene