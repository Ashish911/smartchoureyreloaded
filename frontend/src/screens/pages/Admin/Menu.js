import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { useDispatch, useSelector } from 'react-redux';
import { updateMenuList } from '../../../actions/menuActions';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { toastUI } from '../../../util/util';
import { UPDATE_MENU_LIST_RESET } from '../../../contsants/menuConstants';

const Menu = () => {
    
    const {t} = useTranslation()

    const dispatch = useDispatch();
    const [choureyOneEng, setChoureyOneEng] = useState("");
    const [choureyTwoEng, setChoureyTwoEng] = useState("");
    const [disasterEng, setDisasterEng] = useState("");
    const [choureyOneJap, setChoureyOneJap] = useState("");
    const [choureyTwoJap, setChoureyTwoJap] = useState("");
    const [disasterJap, setDisasterJap] = useState("");

    const menuDetails = useSelector((state) => state.menu);
    const { menuList, menuUpdate } = menuDetails

    const [isLoading, setIsLoading] = useState(false)
    let counter = 0

    const submitHandler = () => {
        dispatch(updateMenuList({
            choureyCustomNameId: menuList.menuList.choureyCustomNameId,
            chourey1: choureyOneEng,
            chourey2: choureyTwoEng,
            disaster: disasterEng,
            saftetyDeclaration: '',
            siteId: menuList.menuList.siteId,
            chourey1Japanese: choureyOneJap,
            chourey2Japanese: choureyTwoJap,
            disasterJapanese: disasterJap,
            saftetyDeclarationJapanese: ''
        }))
    }

    useEffect(() => {
        if (menuList.menuList != undefined) {
            setChoureyOneEng(menuList.menuList.chourey1)
            setChoureyTwoEng(menuList.menuList.chourey2)
            setDisasterEng(menuList.menuList.disaster)
            setChoureyOneJap(menuList.menuList.chourey1Japanese)
            setChoureyTwoJap(menuList.menuList.chourey2Japanese)
            setDisasterJap(menuList.menuList.disasterJapanese)
        }
    }, [menuList])

    useEffect(() => {
        if (menuUpdate) {
            let resp = toastUI(menuUpdate, setIsLoading, "Menu", "updated.")
            if (resp) {
                dispatch({ type: UPDATE_MENU_LIST_RESET })
            }
        }
    }, [menuUpdate])


    return (
        <div
            class="flex flex-col lg:w-2/3 overflow-hidden bg-white rounded-md shadow-lg max md:flex-row md:flex-1"
        >
            {isLoading && <Loader />}
            <div class="flex-1 p-5 bg-white">
                <div className="flex items-start justify-between p-2 border-b border-solid border-slate-200 rounded-t">
                    <h3 className="text-2xl font-semibold">
                    {t("Menu Name.1")}
                    </h3>
                </div>
                <div className='flex flex-1 flex-col sm:flex-row'>
                    <form action="#" class="flex flex-1 flex-row space-y-5 mt-2">
                        <div className='flex flex-1 flex-col p-1'>
                            <h3 className="text-xl font-semibold">
                            English
                            </h3>
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="choureyOneEng" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Chourey 1')}:</label>
                                    <input
                                        type="text"
                                        id="choureyOneEng"
                                        autoFocus
                                        value={choureyOneEng}
                                        onChange={(e) => setChoureyOneEng(e.target.value)}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="choureyTwoEng" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Chourey 2')}:</label>
                                    <input
                                        type="text"
                                        id="choureyTwoEng"
                                        autoFocus
                                        value={choureyTwoEng}
                                        onChange={(e) => setChoureyTwoEng(e.target.value)}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="disasterEng" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Disaster')}:</label>
                                    <input
                                        type="text"
                                        id="disasterEng"
                                        autoFocus
                                        value={disasterEng}
                                        onChange={(e) => setDisasterEng(e.target.value)}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                        </div>
                    </form>
                    <form action="#" class="flex flex-1 flex-row space-y-5 mt-2">
                        <div className='flex flex-1 flex-col p-1'>
                            <h3 className="text-xl font-semibold">
                            日本語
                            </h3>
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="choureyOneJap" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Chourey 1')}:</label>
                                    <input
                                        type="text"
                                        id="choureyOneJap"
                                        autoFocus
                                        value={choureyOneJap}
                                        onChange={(e) => setChoureyOneJap(e.target.value)}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="choureyTwoJap" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Chourey 2')}:</label>
                                    <input
                                        type="text"
                                        id="choureyTwoJap"
                                        autoFocus
                                        value={choureyTwoJap}
                                        onChange={(e) => setChoureyTwoJap(e.target.value)}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                            <div class="flex flex-row space-y-1 items-end">
                                <div className='flex flex-1 flex-col p-1'>
                                    <label for="disasterJap" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Disaster')}:</label>
                                    <input
                                        type="text"
                                        id="disasterJap"
                                        autoFocus
                                        value={disasterJap}
                                        onChange={(e) => setDisasterJap(e.target.value)}
                                        class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                                    />
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                
                <div className='flex items-start p-2'>
                        <button
                            onClick={submitHandler}
                            type="submit"
                            class="px-4 py-2 text-lg font-semibold text-white transition-colors duration-300 bg-cyan-500 rounded-md shadow hover:bg-cyan-600 focus:outline-none focus:ring-cyan-200 focus:ring-4"
                        >
                            {t('Update')}
                        </button>
                    </div>
            </div>
        </div>
    )
}

export default Menu