using System;
using System.Collections.Generic;
using System.Linq;

public class WaveFunctionCollapseMapGenerator
{
    private const int MapSize = 10;
    private static readonly Random Rng = new Random();

    // Define tile types
    public enum TileType
    {
        Empty,
        Obstacle
    }

    // Represents the possibilities for a cell
    [Flags]
    private enum Possibilities
    {
        None = 0,
        Empty = 1 << 0,
        Obstacle = 1 << 1,
        All = Empty | Obstacle
    }

    // The wave function for the entire map
    private Possibilities[,] wave;

    // --- Configuration Variables (can be set by user) ---
    private int seedPatternSize = 4; // Can be 3 or 4
    private TileType[,] seedPattern = new TileType[,]
        // {
        //     { TileType.Empty, TileType.Empty, TileType.Obstacle },
        //     { TileType.Empty, TileType.Obstacle, TileType.Empty },
        //     { TileType.Obstacle, TileType.Empty, TileType.Empty }
        // };
        {
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
            { TileType.Empty, TileType.Obstacle, TileType.Obstacle, TileType.Empty },
            { TileType.Empty, TileType.Empty, TileType.Obstacle, TileType.Empty },
            { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty } 
        };

    public WaveFunctionCollapseMapGenerator()
    {
        InitializeWave();
        // Set seed pattern
        SetSeedPattern();
    }

    // Initialize all cells to have all possibilities
    private void InitializeWave()
    {
        wave = new Possibilities[MapSize, MapSize];
        for (int y = 0; y < MapSize; y++)
        {
            for (int x = 0; x < MapSize; x++)
            {
                wave[x, y] = Possibilities.All;
            }
        }
    }

    private void SetSeedPattern(TileType[,] seed = null)
    {
        if (seed != null)
            seedPattern = seed;
    }

    // Allows setting a custom seed pattern and size
    public void SetCustomSeedPattern(TileType[,] customPattern)
    {
        if (customPattern == null || customPattern.GetLength(0) != customPattern.GetLength(1) ||
            (customPattern.GetLength(0) != 3 && customPattern.GetLength(0) != 4))
        {
            Console.WriteLine("Invalid seed pattern. Must be a 3x3 or 4x4 array.");
            return;
        }
        seedPattern = customPattern;
        seedPatternSize = customPattern.GetLength(0);
        Console.WriteLine($"Seed pattern set to {seedPatternSize}x{seedPatternSize}.");
    }

    // Applies the seed pattern to the center of the map
    private void ApplySeedPattern()
    {
        int startX = (MapSize - seedPatternSize) / 2;
        int startY = (MapSize - seedPatternSize) / 2;

        for (int y = 0; y < seedPatternSize; y++)
        {
            for (int x = 0; x < seedPatternSize; x++)
            {
                Possibilities desiredPossibility = (seedPattern[x, y] == TileType.Empty) ? Possibilities.Empty : Possibilities.Obstacle;
                CollapseCell(startX + x, startY + y, desiredPossibility);
            }
        }
    }

    // Calculates the entropy for a given cell
    private int CalculateEntropy(int x, int y)
    {
        if (x < 0 || x >= MapSize || y < 0 || y >= MapSize) return -1; // Out of bounds
        return GetPossibilityCount(wave[x, y]);
    }

    // Gets the number of remaining possibilities for a cell
    private int GetPossibilityCount(Possibilities p)
    {
        int count = 0;
        if ((p & Possibilities.Empty) != 0) count++;
        if ((p & Possibilities.Obstacle) != 0) count++;
        return count;
    }

    // Finds the cell with the lowest entropy (and not yet fully collapsed)
    private Tuple<int, int> FindLowestEntropyCell()
    {
        int minEntropy = int.MaxValue;
        List<Tuple<int, int>> lowestEntropyCells = new List<Tuple<int, int>>();

        for (int y = 0; y < MapSize; y++)
        {
            for (int x = 0; x < MapSize; x++)
            {
                int currentEntropy = CalculateEntropy(x, y);

                // Skip fully collapsed cells (entropy 1)
                if (currentEntropy == 1) continue;

                if (currentEntropy < minEntropy)
                {
                    minEntropy = currentEntropy;
                    lowestEntropyCells.Clear();
                    lowestEntropyCells.Add(Tuple.Create(x, y));
                }
                else if (currentEntropy == minEntropy)
                {
                    lowestEntropyCells.Add(Tuple.Create(x, y));
                }
            }
        }

        if (lowestEntropyCells.Count == 0) return null; // All cells collapsed
        return lowestEntropyCells[Rng.Next(lowestEntropyCells.Count)]; // Pick a random one from lowest entropy
    }

    // Collapses a specific cell to a chosen possibility
    private void CollapseCell(int x, int y, Possibilities desiredPossibility)
    {
        wave[x, y] = desiredPossibility;
        Propagate(x, y);
    }

    // Collapses the cell with the lowest entropy
    private void CollapseLowestEntropyCell()
    {
        Tuple<int, int> cell = FindLowestEntropyCell();
        if (cell == null) return; // All cells collapsed

        int x = cell.Item1;
        int y = cell.Item2;

        Possibilities currentPossibilities = wave[x, y];
        List<Possibilities> availableChoices = new List<Possibilities>();
        if ((currentPossibilities & Possibilities.Empty) != 0) availableChoices.Add(Possibilities.Empty);
        if ((currentPossibilities & Possibilities.Obstacle) != 0) availableChoices.Add(Possibilities.Obstacle);

        if (availableChoices.Count == 0)
        {
            Console.WriteLine($"Error: No possibilities left for cell ({x},{y}). Contradiction detected!");
            return;
        }

        Possibilities chosen = availableChoices[Rng.Next(availableChoices.Count)];
        CollapseCell(x, y, chosen);
    }

    // Propagates the collapse of a cell to its neighbors
    private void Propagate(int startX, int startY)
    {
        Queue<Tuple<int, int>> propagationQueue = new Queue<Tuple<int, int>>();
        propagationQueue.Enqueue(Tuple.Create(startX, startY));

        while (propagationQueue.Count > 0)
        {
            Tuple<int, int> current = propagationQueue.Dequeue();
            int cx = current.Item1;
            int cy = current.Item2;

            // Check neighbors
            int[] dx = { 0, 0, 1, -1 };
            int[] dy = { 1, -1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int nx = cx + dx[i];
                int ny = cy + dy[i];

                if (nx >= 0 && nx < MapSize && ny >= 0 && ny < MapSize)
                {
                    Possibilities currentNeighborPossibilities = wave[nx, ny];
                    Possibilities originalNeighborPossibilities = currentNeighborPossibilities;

                    // This is a very simplified propagation rule.
                    // In a more complex WFC, you'd have "rules" defining
                    // what tiles can be next to other tiles.
                    // Here, we just ensure consistency based on what's collapsed.

                    // If the current cell is Empty, a neighbor might be either.
                    // If the current cell is Obstacle, a neighbor might be either.
                    // This simple WFC doesn't have complex adjacency rules beyond
                    // ensuring that if a cell is determined, its neighbors
                    // still have valid remaining possibilities.

                    // For this basic example, if a cell is forced to be one type,
                    // its neighbors' possibilities might need to be adjusted.
                    // Let's assume for simplicity, any tile can be next to any other
                    // in terms of "empty/obstacle" but we still need to
                    // ensure that if a neighbor is forced, it doesn't lead to contradiction.

                    // If the current cell (cx, cy) is determined, does it force the neighbor?
                    // Not directly in this simple setup. The propagation is more about
                    // removing possibilities from neighbors if they become inconsistent
                    // with the newly collapsed cell.

                    // Example of a simple constraint: If a tile is an obstacle,
                    // perhaps its immediate neighbors cannot be another obstacle
                    // if that specific rule is desired.

                    // For this simple empty/obstacle WFC, the propagation
                    // primarily happens when a cell's possibilities are *reduced*.
                    // If `wave[cx, cy]` is fully determined, then its neighbors
                    // are re-evaluated to see if any of their possibilities
                    // are no longer valid *given the current state of (cx, cy)*.

                    // Let's implement a very basic rule: an Obstacle can't have *all* its
                    // neighbors be Obstacles if that leads to an unplayable region.
                    // For now, we'll keep it simple and just rely on the entropy reduction.

                    // If a neighbor is fully collapsed, no need to propagate to it.
                    if (GetPossibilityCount(currentNeighborPossibilities) == 1) continue;

                    // This is where more complex WFC rules would go.
                    // For instance, if you have a rule that an 'Empty' tile
                    // can only be next to another 'Empty' tile, you'd prune
                    // 'Obstacle' from the neighbor's possibilities if (cx, cy)
                    // became 'Empty'.

                    // As this example doesn't have complex adjacency rules beyond
                    // what's implied by the initial state, the propagation
                    // will mostly just ensure that a cell doesn't get into an
                    // impossible state.

                    // For a fully functional WFC, you would have a set of "patterns"
                    // or "tiles" with defined adjacencies. When a cell collapses,
                    // you would look at the remaining valid patterns for its neighbors
                    // and remove any possibilities that no longer align.

                    // In this basic version, a cell's collapse simply reduces its
                    // own entropy, which then makes it a candidate for finding the
                    // next lowest entropy. The "propagation" here is more about
                    // re-evaluating neighbors to ensure consistency, but without
                    // complex rules, it's mostly a check.

                    // Let's simplify and say: if a neighbor (nx, ny) had a possibility that
                    // is now inconsistent with the state of (cx, cy), remove it.
                    // Example: If (cx, cy) is Obstacle and you have a rule that
                    // Obstacles cannot be adjacent, then if (nx, ny) is still
                    // considering Obstacle, you would remove it.

                    // For now, we'll let the lowest entropy selection do most of the work,
                    // and just ensure no cell ends up with `None` possibilities.
                    if (GetPossibilityCount(wave[cx, cy]) == 1) // If current cell is fully collapsed
                    {
                        // No specific adjacency rules for Empty/Obstacle in this simplified version
                        // The primary propagation effect comes from reducing possibilities
                        // which then makes other cells have lower entropy.
                    }

                    // If a neighbor's possibilities changed, add it to the queue
                    if (currentNeighborPossibilities != wave[nx, ny])
                    {
                        propagationQueue.Enqueue(Tuple.Create(nx, ny));
                    }
                }
            }
        }
    }

    // Generates the map using WFC
    public void GenerateMap()
    {
        ApplySeedPattern(); // Start with the seed

        while (true)
        {
            Tuple<int, int> lowestEntropyCell = FindLowestEntropyCell();
            if (lowestEntropyCell == null) // All cells collapsed
            {
                Console.WriteLine("Map generation complete.");
                break;
            }

            CollapseLowestEntropyCell();
        }
    }

    // Displays the generated map on the console
    public void DisplayMap()
    {
        Console.WriteLine("\nGenerated Map:");
        for (int y = 0; y < MapSize; y++)
        {
            for (int x = 0; x < MapSize; x++)
            {
                if (GetPossibilityCount(wave[x, y]) == 1)
                {
                    if ((wave[x, y] & Possibilities.Empty) != 0)
                    {
                        Console.Write(". "); // Empty tile
                    }
                    else if ((wave[x, y] & Possibilities.Obstacle) != 0)
                    {
                        Console.Write("# "); // Obstacle tile
                    }
                    else
                    {
                        Console.Write("? "); // Should not happen
                    }
                }
                else
                {
                    Console.Write("? "); // Uncollapsed or error
                }
            }
            Console.WriteLine();
        }
    }

    public void DisplaySeed()
    {
        Console.WriteLine("\nSeed Pattern:");
        for (int i = 0; i < seedPatternSize; i++)
        {
            for (int j = 0; j < seedPatternSize; j++)
            {
                if (seedPattern[i, j] == TileType.Empty)
                {
                    Console.Write(". "); // Empty tile
                }
                else if (seedPattern[i, j] == TileType.Obstacle)
                {
                    Console.Write("# "); // Obstacle tile
                }
                else
                {
                    Console.Write("? "); // Should not happen
                }
            }
            Console.WriteLine();
        }
    }

    public static void Main(string[] args)
    {
        char key = 'y';
        do
        {
            Console.WriteLine("Starting Wave Function Collapse Map Generation (10x10)");
            WaveFunctionCollapseMapGenerator generator = new WaveFunctionCollapseMapGenerator();

            // --- User Configuration ---
            // You can uncomment and modify these lines to change the seed pattern
            // Example 1: A 3x3 pattern with more obstacles
            // TileType[,] customSeed3x3 = new TileType[3, 3]
            // {
            //     { TileType.Obstacle, TileType.Empty, TileType.Obstacle },
            //     { TileType.Empty, TileType.Obstacle, TileType.Empty },
            //     { TileType.Obstacle, TileType.Empty, TileType.Obstacle }
            // };
            // generator.SetCustomSeedPattern(customSeed3x3);

            // Example 2: A 4x4 pattern with a clear path
            // TileType[,] customSeed4x4 = new TileType[4, 4]
            // {
            //     { TileType.Obstacle, TileType.Obstacle, TileType.Empty, TileType.Obstacle },
            //     { TileType.Obstacle, TileType.Empty, TileType.Empty, TileType.Obstacle },
            //     { TileType.Obstacle, TileType.Empty, TileType.Empty, TileType.Obstacle },
            //     { TileType.Obstacle, TileType.Obstacle, TileType.Empty, TileType.Obstacle }
            // };
            // generator.SetCustomSeedPattern(customSeed4x4);

            generator.GenerateMap();
            generator.DisplayMap();
            generator.DisplaySeed();

            Console.WriteLine("\nGenerate again? (y/n)");
            key = Console.ReadKey().KeyChar;

        } while (key != 'n');
    }
}