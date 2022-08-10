import { ApiConfig } from "../../index";

interface GetBoardInfoRequest {
  gameBoard: number[][];
  row: number;
  column: number;
}

interface GetBoardInfoResponse {
  blackChessScore: number;
  whiteChessScore: number;
  // blackWin: boolean;
  // whiteWin: boolean;
  // blackHasLiveFour: boolean;
  // whiteHasLiveFour: boolean;
  // blackHasDoubleLiveThree: boolean;
  // whiteHasDoubleLiveThree: boolean;
  // blackHasDoubleDeadFour: boolean;
  // whiteHasDoubleDeadFour: boolean;
  // blackHasDeadFourLiveThree: boolean;
  // whiteHasDeadFourLiveThree: boolean;
  // blackMustFollow: boolean;
  // whiteMustFollow: boolean;
}

const getBoardInfoApi = (
  gameBoard: number[][],
  row: number,
  column: number,
  onError: (e: unknown) => void
): ApiConfig<GetBoardInfoRequest> => {
  return {
    method: "post",
    url: "/api/goBang/GetBoardInfo",
    params: {
      gameBoard: gameBoard,
      row: row,
      column: column,
    } as GetBoardInfoRequest,
    config: undefined,
    onError: onError,
  } as ApiConfig<GetBoardInfoRequest>;
};

export { GetBoardInfoRequest, GetBoardInfoResponse, getBoardInfoApi };
