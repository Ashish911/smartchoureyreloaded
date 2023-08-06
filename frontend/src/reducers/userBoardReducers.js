import { GET_USERBOARD_FAIL, GET_USERBOARD_REQUEST, GET_USERBOARD_RESET, GET_USERBOARD_SUCCESS } from "../contsants/userBoardConstants";


export const getUserBoardReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_USERBOARD_REQUEST:
            return {
                loading: true
            }
        case GET_USERBOARD_SUCCESS:
            return {
                loading: false,
                success: true,
                board: action.payload 
            };
        case GET_USERBOARD_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_USERBOARD_RESET:
            return {}
        default:
            return state;
    }
}
