import { 
    MENU_LIST_FAIL, 
    MENU_LIST_REQUEST, 
    MENU_LIST_RESET, 
    MENU_LIST_SUCCESS, 
    UPDATE_MENU_LIST_FAIL, 
    UPDATE_MENU_LIST_REQUEST, 
    UPDATE_MENU_LIST_RESET, 
    UPDATE_MENU_LIST_SUCCESS 
} from "../contsants/menuConstants";


export const getMenuListReducer = (state = {}, action) => {
    switch (action.type) {
        case MENU_LIST_REQUEST:
            return {
                loading: true
            }
        case MENU_LIST_SUCCESS:
            return {
                loading: false,
                success: true,
                menuList: action.payload 
            };
        case MENU_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case MENU_LIST_RESET:
            return {}
        default:
            return state;
    }
}

export const updateMenuListReducer = (state = {}, action) => {
    switch (action.type) {
        case UPDATE_MENU_LIST_REQUEST:
            return {
                loading: true
            }
        case UPDATE_MENU_LIST_SUCCESS:
            return {
                loading: false,
                success: true 
            };
        case UPDATE_MENU_LIST_FAIL:
            return {
                loading: false,
                error: action.payload
            };
        case UPDATE_MENU_LIST_RESET:
            return {}
        default:
            return state;
    }
}