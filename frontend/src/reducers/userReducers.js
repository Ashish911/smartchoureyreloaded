import {
    USER_CREATE_FAIL,
    USER_CREATE_REQUEST,
    USER_CREATE_RESET,
    USER_CREATE_SUCCESS,
    USER_DETAILS_FAIL,
    USER_DETAILS_REQUEST,
    USER_DETAILS_RESET,
    USER_DETAILS_SUCCESS,
    USER_FORGOT_PASSWORD_FAIL,
    USER_FORGOT_PASSWORD_REQUEST,
    USER_FORGOT_PASSWORD_RESET,
    USER_FORGOT_PASSWORD_SUCCESS,
    USER_LIST_FAIL,
    USER_LIST_REQUEST,
    USER_LIST_RESET,
    USER_LIST_SUCCESS,
    USER_LOGIN_FAIL,
    USER_LOGIN_REQUEST,
    USER_LOGIN_SUCCESS,
    USER_LOGOUT,
    USER_REGISTER_FAIL,
    USER_REGISTER_REQUEST,
    USER_REGISTER_RESET,
    USER_REGISTER_SUCCESS,
    USER_RESET_PASSWORD_FAIL,
    USER_RESET_PASSWORD_REQUEST,
    USER_RESET_PASSWORD_RESET,
    USER_RESET_PASSWORD_SUCCESS,
    USER_UPDATE_PASSWORD_FAIL,
    USER_UPDATE_PASSWORD_REQUEST,
    USER_UPDATE_PASSWORD_RESET,
    USER_UPDATE_PASSWORD_SUCCESS,
    USER_UPDATE_PROFILE_FAIL,
    USER_UPDATE_PROFILE_REQUEST,
    USER_UPDATE_PROFILE_RESET,
    USER_UPDATE_PROFILE_SUCCESS
} from '../contsants/userConstants'

export const userLoginReducer = (state = {}, action) => {
    switch (action.type) {
        case USER_LOGIN_REQUEST:
            return {
                loading: true
            };
        case USER_LOGIN_SUCCESS:
            return {
                loading: false,
                userInfo: action.payload
            };
        case USER_LOGIN_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case USER_LOGOUT:
            return {};
        default:
            return state;
    }
}

export const userRegisterReducer = (state = {}, action) => {
    switch (action.type) {
        case USER_REGISTER_REQUEST:
            return {
                loading: true
            };
        case USER_REGISTER_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case USER_REGISTER_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case USER_REGISTER_RESET:
            return {}
        default:
            return state;
    }
};

export const userDetailsReducer = (state = { userDetail: {} }, action) => {
    switch (action.type) {
        case USER_DETAILS_REQUEST:
            return {
                ...state,
                loading: true
            };
        case USER_DETAILS_SUCCESS:
            return {
                loading: false,
                userDetail: action.payload
            };
        case USER_DETAILS_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case USER_DETAILS_RESET:
            return {
                userDetail: {}
            };
        default:
            return state;
    }
};

export const userUpdateProfileReducer = (state = {}, action) => {
    switch (action.type) {
        case USER_UPDATE_PROFILE_REQUEST:
            return {
                ...state,
                loading: true
            };
        case USER_UPDATE_PROFILE_SUCCESS:
            return {
                loading: false,
                success: true,
                info: action.payload
            };
        case USER_UPDATE_PROFILE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case USER_UPDATE_PROFILE_RESET:
            return {}
        default:
            return state;
    }
};

export const userUpdatePasswordReducer = (state = {}, action) => {
    switch (action.type) {
        case USER_UPDATE_PASSWORD_REQUEST:
            return {
                ...state,
                loading: true
            };
        case USER_UPDATE_PASSWORD_SUCCESS:
            return {
                loading: false,
                success: true,
                info: action.payload
            };
        case USER_UPDATE_PASSWORD_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case USER_UPDATE_PASSWORD_RESET:
            return {}
        default:
            return state;
    }
};

export const userListReducer = (state = { users: [] }, action) => {
    switch (action.type) {
        case USER_LIST_REQUEST:
            return {
                ...state,
                loading: true
            };
        case USER_LIST_SUCCESS:
            return {
                loading: false,
                users: action.payload
            };
        case USER_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case USER_LIST_RESET:
            {}
        default:
            return state;
    }
};

export const createUserReducer = (state = {}, action) => {
    switch (action.type) {
        case USER_CREATE_REQUEST:
            return {
                ...state,
                loading: true
            };
        case USER_CREATE_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case USER_CREATE_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case USER_CREATE_RESET:
            return {}
        default:
            return state;
    }
};

export const forgotPasswordReducer = (state = {}, action) => {
    switch (action.type) {
        case USER_FORGOT_PASSWORD_REQUEST:
            return {
                ...state,
                loading: true
            };
        case USER_FORGOT_PASSWORD_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case USER_FORGOT_PASSWORD_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case USER_FORGOT_PASSWORD_RESET:
            return {}
        default:
            return state;
    }
};

export const resetPasswordReducer = (state = {}, action) => {
    switch (action.type) {
        case USER_RESET_PASSWORD_REQUEST:
            return {
                ...state,
                loading: true
            };
        case USER_RESET_PASSWORD_SUCCESS:
            return {
                loading: false,
                success: true
            };
        case USER_RESET_PASSWORD_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case USER_RESET_PASSWORD_RESET:
            return {}
        default:
            return state;
    }
};