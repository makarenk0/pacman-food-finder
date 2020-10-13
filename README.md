# pacman-food-finder
Implemented algorithms:<br>
#### Non informed: "Iterative deepening depth-first search algorithm" and "Breadth First Search"<br>
#### Informed: "A* algorithm" and "Greedy algorithm"<br>
Settings:
- In Pacman.cs , row 34 you choose algorithm type **DFS_with_iter_deeping** or **BFS**
- In the next row you can change **DEFAULT_DFS_LIMIT**
- In the next row you can change speed of pacman,  decrease **DEFAULT_PACMAN_SPEED** to make pacman faster and vice versa
- In the next row you can change draw solution speed(only for informed algorithms), decrease **SOLUTION_DRAW_SPEED** to make animation faster and vice versa
- In the next row you can change game map, **LEVEL_NUMBER** could be 1 (original pacman map) or 2 (map for testing the difference between greedy and A* algorithms)
- Additionally in GameBoard.cs you can find map matrix. If you don't want to spawn pacman randomly, just change one cell of the matrix from _**00**_ (empty cells where pacman can move) to _**03**_ (pacman code) 
<br><br>All statistics information is displayed in console
