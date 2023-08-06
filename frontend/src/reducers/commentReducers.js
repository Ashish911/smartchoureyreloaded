import { ADD_COMMENT_FAIL, ADD_COMMENT_REQUEST, ADD_COMMENT_RESET, ADD_COMMENT_SUCCESS, DELETE_COMMENT_FAIL, DELETE_COMMENT_REQUEST, DELETE_COMMENT_RESET, DELETE_COMMENT_SUCCESS, GET_COMMENT_BY_MEDIA_FAIL, GET_COMMENT_BY_MEDIA_REQUEST, GET_COMMENT_BY_MEDIA_RESET, GET_COMMENT_BY_MEDIA_SUCCESS } from "../contsants/commentConstants";

export const getCommentsListReducer = (state = {}, action) => {
    switch (action.type) {
        case GET_COMMENT_BY_MEDIA_REQUEST:
            return {
                loading: true
            }
        case GET_COMMENT_BY_MEDIA_SUCCESS:
            return {
                loading: false,
                success: true,
                list: action.payload 
            };
        case GET_COMMENT_BY_MEDIA_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case GET_COMMENT_BY_MEDIA_RESET:
            return {}
        default:
            return state;
    }
}

export const deleteCommentReducer = (state = {}, action) => {
    switch (action.type) {
        case DELETE_COMMENT_REQUEST:
            return {
                loading: true
            }
        case DELETE_COMMENT_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case DELETE_COMMENT_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case DELETE_COMMENT_RESET:
            return {}
        default:
            return state;
    }
}

export const addCommentReducer = (state = {}, action) => {
    switch (action.type) {
        case ADD_COMMENT_REQUEST:
            return {
                loading: true
            }
        case ADD_COMMENT_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case ADD_COMMENT_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case ADD_COMMENT_RESET:
            return {}
        default:
            return state;
    }
}