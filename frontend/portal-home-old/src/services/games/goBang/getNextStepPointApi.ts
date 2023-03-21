import { ApiConfig } from "../../index";

interface GetNextStepPointRequest {
  gameBoard: number[][];
  row: number;
  column: number;
  deep: number;
}

interface GetNextStepPointResponse {
  row: number;
  column: number;
  gameStatus: string;
}

const getNextStepPointApi = (
  gameBoard: number[][],
  row: number,
  column: number,
  deep: number,
  onError: (e: unknown) => void
): ApiConfig<GetNextStepPointRequest> => {
  return {
    method: "post",
    url: "/api/goBang/GetNextStepPoint",
    params: {
      gameBoard: gameBoard,
      row: row,
      column: column,
      deep: deep,
    } as GetNextStepPointRequest,
    config: undefined,
    onError: onError,
  } as ApiConfig<GetNextStepPointRequest>;
};

export { GetNextStepPointRequest, GetNextStepPointResponse, getNextStepPointApi };
