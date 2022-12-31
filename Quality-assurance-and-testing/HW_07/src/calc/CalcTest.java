package calc;

import com.code_intelligence.jazzer.api.FuzzedDataProvider;
import static calc.Calc.*;

public class CalcTest {
    public static void fuzzerTestOneInput(FuzzedDataProvider data){
        try {
            calculate(data.consumeRemainingAsString());
        } catch (CalcException e){
            e.printStackTrace();
        }
    }
}
