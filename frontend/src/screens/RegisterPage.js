import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import {
  useLocation,
  useNavigate,
  Link
} from "react-router-dom";
import logo from '../Images/logo-english.png';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import logoJapanese from '../Images/logo-japanese.png';
import copyright from '../Images/qweqwe.png';
import { useTranslation } from 'react-i18next'
import { register } from '../actions/userActions';
import Loader from './elements/Loader';
import { validation } from '../util/util';
import { USER_REGISTER_RESET } from '../contsants/userConstants';

const RegisterPage = ({onchange, i18next}) => {

  let location = useLocation();
  let history = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [termsRead, setTermsRead] = useState(false)

  const dispatch = useDispatch();

  const [showModal, setShowModal] = useState(false)

  const {t} = useTranslation()

  const userLogin = useSelector((state) => state.user);
  const { loading, error, userInfo } = userLogin

  const registerInfo = useSelector((state) => state.registerUser);

  const [isLoading, setIsLoading] = useState(false)

  const redirect = location.search ? location.search.split('=')[1] : '/'

  useEffect(() => {
    if (userInfo) {
      if (userInfo.role == 'Admin' || userInfo.role == "SubAdmin") {
        history('/dashboard')
      } else if (userInfo.role == "SuperAdmin") {
        history('/adminDashboard')
      } else if (userInfo.role == "User") {
        history('/userDashboard')
      }
    }
  }, [history, userInfo, redirect])

  const submitHandler = (e) => {
    e.preventDefault();
    let validated = validation(email, password, confirmPassword, termsRead)
    if (validated == true) {
      dispatch(register(email, password, confirmPassword))
    }
  }

  useEffect(() => {
    if (registerInfo) {
      setIsLoading(registerInfo.loading)
      if (registerInfo.error) {
        toast.error(registerInfo.error, {
          position: toast.POSITION.TOP_RIGHT
        })
        dispatch({ type: USER_REGISTER_RESET })
      }
    }
  }, [registerInfo])

  return (
    <>
      {isLoading && <Loader />}
      <div class="flex items-center min-h-screen p-4 bg-cyan-600 lg:justify-center">
        <div
          class="flex flex-col overflow-hidden bg-white rounded-md shadow-lg max md:flex-row md:flex-1 lg:max-w-screen-md"
        >
          <div
            class="p-4 py-6 text-white bg-gray-200 md:w-80 md:flex-shrink-0 md:flex md:flex-col md:items-center md:justify-evenly"
          >
            <div className="relative rounded-md border-gray-300 text-cyan-600 bg-white shadow-lg w-2/5">
                <label for="frm-whatever" className="sr-only">My field</label>
                <select className="appearance-none w-full py-1 px-2 bg-white" name="whatever" id="frm-whatever" onChange={(e) => onchange(e)} value={i18next.language}>
                  <option value="en">English</option>
                  <option value="ja">日本語</option>
                </select>
                <div className="pointer-events-none absolute right-0 top-0 bottom-0 flex items-center px-2 text-cyan-700 border-l ml-2">
                    <svg className="h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
                        <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
                    </svg>
                </div>
            </div>
            <div class="my-3 text-4xl font-bold tracking-wider text-center">
              {i18next.language == 'en' || i18next.language == 'en-US' ?
                <img src={logo} alt="React Image" /> :
                <img src={logoJapanese} alt="React Image" />
              }
            </div>
            <p className="mt-6 text-sm text-center text-cyan-600">
              Copyright@2020
              <img src={copyright} alt="React Image" />
            </p>
          </div>
          <div class="p-5 bg-white md:flex-1">
            <h3 class="my-4 text-2xl font-semibold text-gray-700">{t('SignUp.1')}</h3>
            <form action="#" class="flex flex-col space-y-5">
              <div class="flex flex-col space-y-1">
                <label for="email" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Email Address.1')}</label>
                <input
                  type="email"
                  id="email"
                  autoFocus
                  onChange={(e) => setEmail(e.target.value)}
                  class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                />
              </div>
              <div class="flex flex-col space-y-1">
                <div class="flex items-center justify-between">
                  <label for="password" class="text-sm font-semibold text-gray-500">{t('Password.1')}</label>
                </div>
                <input
                  type="password"
                  id="password"
                  onChange={(e) => setPassword(e.target.value)}
                  class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                />
              </div>
              <div class="flex flex-col space-y-1">
                <div class="flex items-center justify-between">
                  <label for="confirmPassword" class="text-sm font-semibold text-gray-500">{t('Confirm Password.1')}</label>
                </div>
                <input
                  type="password"
                  id="confirmPassword"
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                />
              </div>
              <div class="flex items-center space-x-2">
                <input
                  type="checkbox"
                  id="remember"
                  onChange={(e) => setTermsRead(!termsRead)}
                  class="w-4 h-4 transition duration-300 rounded focus:ring-2 focus:ring-offset-0 focus:outline-none focus:ring-cyan-200"
                />
                  <p className="text-sm text-center text-cyan-500" onClick={() => setShowModal(true)}>
                  {t('Accept our terms and conditions.1')}
                  </p>
              </div>
              <div>
                <button
                  onClick={submitHandler}
                  type="submit"
                  class="w-full px-4 py-2 text-lg font-semibold text-white transition-colors duration-300 bg-cyan-500 rounded-md shadow hover:bg-cyan-600 focus:outline-none focus:ring-cyan-200 focus:ring-4"
                >
                  {t('SignUp.1')}
                </button>
              </div>
              <div class="flex flex-col space-y-5">
                <span class="flex items-center justify-center space-x-2">
                  <span class="h-px bg-gray-400 w-14"></span>
                  <span class="font-normal text-gray-500">{t('or.1')}</span>
                  <span class="h-px bg-gray-400 w-14"></span>
                </span>
              </div>
              <div className="flex flex-col space-y-5">
                <span className="flex items-center justify-center space-x-2">
                  <span className="font-normal text-gray-500">{t("Already have an account.1")}</span>
                  <Link to={'/'} className="font-normal text-cyan-400">{t("Login Now?.1")}</Link>
                </span>
              </div>
            </form>
          </div>
        </div>
        {showModal ? (
          <>
            <div
              className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none"
            >
              <div className="relative w-auto my-6 mx-auto max-w-3xl">
                <div className="border-0 rounded-lg shadow-lg relative flex flex-col w-full bg-white outline-none focus:outline-none">
                  <div className="flex items-start justify-between p-5 border-b border-solid border-slate-200 rounded-t">
                    <h3 className="text-3xl font-semibold">
                    {t("Terms and Condition.1")}
                    </h3>
                    <button
                      className="p-1 ml-auto bg-transparent border-0 text-black opacity-5 float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                      onClick={() => setShowModal(false)}
                    >
                      <span className="bg-transparent text-black opacity-5 h-6 w-6 text-2xl block outline-none focus:outline-none">
                        ×
                      </span>
                    </button>
                  </div>
                  <div className="relative p-6 flex">
                    <div className='just'>
                      <b>{t("Terms and Condition.1")}</b>
                      <p> </p>
                      <ol>
                        <li>
                          <b>{t("terms.1name")}</b>
                          <br/>
                          <p>{t("terms.1")}</p>
                        </li>
                      </ol>
                    </div>
                  </div>
                  <div className="flex items-center justify-end p-6 border-t border-solid border-slate-200 rounded-b">
                    <button
                      className="text-red-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"
                      type="button"
                      onClick={() => setShowModal(false)}
                    >
                      <p>{t("Close.1")}</p>
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
          </>
        ) : null}
      </div>
    </>
  )
}

export default RegisterPage