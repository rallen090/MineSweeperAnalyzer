import java.util.Scanner;
import com.google.gson.Gson;

public class RemoteAdapter {
    private Solver _solver;

    public RemoteAdapter(Solver solver)
    {
        this._solver = solver;
    }

    public void HandleMessages() throws SolverImplementationException
    {
    	Gson gson = new Gson();
    	try(Scanner standardInput = new Scanner(System.in)){
    		String message;
            while ((message = standardInput.nextLine()) != null)
            {
            	Cell[][] grid = gson.fromJson(message, Cell[][].class);
                Move move;
                try
                {
                    move = this._solver.GetNextMove(grid);
                }
                catch (Exception ex)
                {
                    throw new SolverImplementationException("Error thrown by Solve() implementation!", ex);
                }
                System.out.println(gson.toJson(move));
            }
    	};
    }
}
