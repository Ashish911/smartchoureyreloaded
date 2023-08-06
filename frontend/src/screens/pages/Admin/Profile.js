import React, { useEffect, useState } from 'react'
import InitialsAvatar from 'react-initials-avatar';
import 'react-initials-avatar/lib/ReactInitialsAvatar.css';
import { useTranslation } from 'react-i18next'
import i18next from 'i18next'
import DatePicker from 'react-datepicker'
import "react-datepicker/dist/react-datepicker.css";
import {
    Link
} from "react-router-dom";
import { getProfile, updateProfile } from '../../../actions/userActions';
import { useDispatch, useSelector } from 'react-redux';
import moment from 'moment';
import { toast } from 'react-toastify';
import Loader from '../../elements/Loader'
import { toastUI } from '../../../util/util';
import { USER_UPDATE_PROFILE_RESET } from '../../../contsants/userConstants';

const Profile = () => {

    const dispatch = useDispatch();

    const [kanjiFirstName, setKanjiFirstName] = useState('');
    const [kanjiLastName, setKanjiLastName] = useState('');
    const [kanaFirstName, setKanaFirstName] = useState('');
    const [kanaLastName, setKanaLastName] = useState('');
    const [companyName, setCompanyName] = useState('');
    const [companyAddress, setCompanyAddress] = useState('');
    const [mobileNumber, setMobileNumber] = useState('');
    const [emergencyContact, setEmergencyContact] = useState('');
    const [postalCode, setPostalCode] = useState('');
    const [prefecture, setPrefecture] = useState('');
    const [city, setCity] = useState('');
    const [address, setAddress] = useState('');
    const [startDate, setStartDate] = useState(new Date());
    const [selectedValue, setSelectedValue] = useState("");
    const [selectedCountryValue, setSelectedCountryValue] = useState("");
    const userDetails = useSelector((state) => state.user);
    const { userInfo } = userDetails

    const userProfile = useSelector((state) => state.profile);
    const { detail, update } = userProfile

    const [isLoading, setIsLoading] = useState(false)

    const {t} = useTranslation()

    useEffect(() => {
        if (userInfo != undefined) {
            dispatch(getProfile(userInfo.id))
        }
    }, [userInfo])

    useEffect(() => {
        if (Object.keys(detail?.userDetail).length != 0) {
            setKanjiFirstName(detail?.userDetail?.familyName_Chinese)
            setKanjiLastName(detail?.userDetail?.givenName_Chinese)
            setKanaFirstName(detail?.userDetail?.familyName_Kana)
            setKanaLastName(detail?.userDetail?.givenName_Kana)
            setCompanyName(detail?.userDetail?.familyName_Roman)
            setCompanyAddress(detail?.userDetail?.givenName_Roman)
            setMobileNumber(detail?.userDetail?.mobileNumber)
            setEmergencyContact(detail?.userDetail?.emergencyContactNumber)
            setPostalCode(detail?.userDetail?.postbox)
            setPrefecture(detail?.userDetail?.prefecture)
            setCity(detail?.userDetail?.city)
            setAddress(detail?.userDetail?.address)
            setSelectedCountryValue(detail?.userDetail?.country)
            setSelectedValue(detail?.userDetail?.gender)
            setStartDate(moment(detail?.userDetail?.dob, 'YYYY-MM-DD').toDate())
        }
    },[detail])

    const submitHandler = (e) => {
        let params = {
            dob: moment(startDate, 'ddd MMM DD YYYY HH:mm:ss [GMT]ZZ').format("YYYY/MM/DD"),
            userId: userInfo.id,
            employeeId: detail?.userDetail?.employeeId,
            kanaLastName: kanaLastName,
            kanaFirstName: kanaFirstName,
            kanjiFirstName: kanjiFirstName,
            kanjiLastName: kanjiLastName,
            companyName: companyName,
            companyAddress: companyAddress,
            gender: selectedValue,
            country: selectedCountryValue,
            mobileNumber: mobileNumber,
            emergencyContactNumber: emergencyContact,
            postalCode: postalCode,
            prefecture: prefecture,
            city: city,
            address: address,
            email: userInfo.email
        }
        dispatch(updateProfile(params))
    }

    useEffect(() => {
        if (update) {
            let resp = toastUI(update, setIsLoading, "Profile", "updated.")
            if (resp) {
                dispatch({ type: USER_UPDATE_PROFILE_RESET })
            }
        }
    }, [update])

    return (
        <div x-data="lw" class="min-h-screen flex flex-col justify-evenly lg:flex-row">
            {isLoading && <Loader />}
            <div class="customizableProfile w-full max-w-[300px] max-h-[400px] bg-white p-8 rounded-xl text-gray-800 overflow-hidden group border-2 border-gray-200 shadow-md shadow-gray-200/50
                        hover:shadow-2xl hover:shadow-sky-500/50 hover:border-transparent motion-safe:transition-all motion-safe:duration-700">
                <figure class="relative w-40 h-40 m-0 mx-auto rounded-full group-hover:outline group-hover:outline-offset-4 group-hover:outline-sky-500
                    bg-cyan-500 motion-safe:transition-all motion-safe:duration-400"
                        >
                    <InitialsAvatar className="flex w-40 h-40 justify-center items-center text-white text-4xl" name={detail?.userDetail?.familyName_Chinese + ' ' + detail?.userDetail?.givenName_Chinese}/>
                </figure>
                <header>
                    <h3 class="font-semibold text-2xl text-center text-gray-400 mt-6 group-hover:text-sky-500 relative motion-safe:transition-all motion-safe:duration-700">{detail?.userDetail?.familyName_Chinese + ' ' + detail?.userDetail?.givenName_Chinese}</h3>
                    <p class="text-center text-gray-400 group-hover:text-sky-500 relative motion-safe:transition-all motion-safe:duration-700">{userInfo?.email}</p>
                    <p class="text-center text-gray-400 group-hover:text-sky-500 relative motion-safe:transition-all motion-safe:duration-700">Registered Date : {detail?.userDetail?.dateOfRegister}</p>
                    <p className="text-center text text-gray-400 group-hover:text-sky-500 relative motion-safe:transition-all motion-safe:duration-700">
                        Our Privacy Policy
                    </p>
                </header>
            </div>
            <div
                class="flex flex-col overflow-hidden bg-white rounded-md shadow-lg max md:flex-row md:flex-1 lg:max-w-screen-md"
            >
                <div class="flex-1 p-5 bg-white">
                <form action="#" class="flex flex-col space-y-5">
                    <div class="flex flex-col sm:flex-row space-y-1 sm:items-end">
                        <div className='flex flex-1 flex-col p-1 max-w'>
                            <label for="kanji" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Kanji (Family Name)')}:*</label>
                            <input
                                type="text"
                                id="kanji"
                                value={kanjiFirstName}
                                onChange={(e) => setKanjiFirstName(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                        <div className='flex flex-1 flex-col p-1'>
                            <input
                                type="text"
                                id="kanji"
                                value={kanjiLastName}
                                onChange={(e) => setKanjiLastName(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div class="flex flex-col sm:flex-row space-y-1 sm:items-end">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="kana" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Kana (Family Name)')}:*</label>
                            <input
                                type="text"
                                id="kana"
                                value={kanaFirstName}
                                onChange={(e) => setKanaFirstName(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                        <div className='flex flex-1 flex-col p-1'>
                            <input
                                type="text"
                                id="kana"
                                value={kanaLastName}
                                onChange={(e) => setKanaLastName(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div class="flex flex-col sm:flex-row space-y-1 sm:items-end">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="companyName" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Company Name')}:*</label>
                            <input
                                type="text"
                                id="companyName"
                                value={companyName}
                                onChange={(e) => setCompanyName(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="companyAddress" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Company Address')}:*</label> 
                            <input
                                type="text"
                                id="companyAddress"
                                value={companyAddress}
                                onChange={(e) => setCompanyAddress(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div class="flex flex-col sm:flex-row space-y-1 sm:items-end">
                        <div class="flex flex-1 flex-col p-1">
                            <div class="flex items-center justify-between">
                                <label for="dateOfBirth" class="text-sm font-semibold text-gray-500">{t('Date Of Birth')}</label>
                            </div>
                            <DatePicker className='border flex-1 border-gray-300 rounded px-4 py-2 w-full' id="dateOfBirth" selected={startDate} onChange={(date) => setStartDate(date)} />
                        </div>
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="gender" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Gender')}:*</label> 
                            <select
                                value={selectedValue}
                                onChange={event => setSelectedValue(event.target.value)}
                                className="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline"
                            >
                                <option value="male">Male</option>
                                <option value="female">Female</option>
                            </select>
                        </div>
                    </div>
                    <div class="flex flex-col sm:flex-row space-y-1 sm:items-end">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="phoneNo" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Phone / Mobile No')}:*</label>
                            <input
                                type="text"
                                id="phoneNo"
                                value={mobileNumber}
                                onChange={(e) => setMobileNumber(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="emergencyContact" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Emergency Contact')}:*</label> 
                            <input
                                type="text"
                                id="emergencyContact"
                                value={emergencyContact}
                                onChange={(e) => setEmergencyContact(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div class="flex flex-row space-y-1 items-end">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="countryName" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Country Name')}:*</label> 
                            <select
                                value={selectedCountryValue}
                                onChange={event => setSelectedCountryValue(event.target.value)}
                                className="block appearance-none w-full bg-white border border-gray-400 hover:border-gray-500 px-4 py-2 pr-8 rounded shadow leading-tight focus:outline-none focus:shadow-outline"
                            >
                                <option value="Afghanistan">Afghanistan</option>
                                <option value="Albania">Albania</option>
                                <option value="Algeria">Algeria</option>
                                <option value="American Samoa">American Samoa</option>
                                <option value="Andorra">Andorra</option>
                                <option value="Angola">Angola</option>
                                <option value="Anguilla">Anguilla</option>
                                <option value="Antarctica">Antarctica</option>
                                <option value="Antigua and Barbuda">Antigua and Barbuda</option>
                                <option value="Argentina">Argentina</option>
                                <option value="Armenia">Armenia</option>
                                <option value="Aruba">Aruba</option>
                                <option value="Australia">Australia</option>
                                <option value="Austria">Austria</option>
                                <option value="Azerbaijan">Azerbaijan</option>
                                <option value="Bahamas">Bahamas</option>
                                <option value="Bahrain">Bahrain</option>
                                <option value="Bangladesh">Bangladesh</option>
                                <option value="Barbados">Barbados</option>
                                <option value="Belarus">Belarus</option>
                                <option value="Belgium">Belgium</option>
                                <option value="Belize">Belize</option>
                                <option value="Benin">Benin</option>
                                <option value="Bermuda">Bermuda</option>
                                <option value="Bhutan">Bhutan</option>
                                <option value="Bolivia">Bolivia</option>
                                <option value="Bonaire">Bonaire</option>
                                <option value="Bosnia and Herzegovina">Bosnia and Herzegovina</option>
                                <option value="Botswana">Botswana</option>
                                <option value="Bouvet Island">Bouvet Island</option>
                                <option value="Brazil">Brazil</option>
                                <option value="British Indian Ocean Territory">British Indian Ocean Territory</option>
                                <option value="British Virgin Islands">British Virgin Islands</option>
                                <option value="Brunei">Brunei</option>
                                <option value="Bulgaria">Bulgaria</option>
                                <option value="Burkina Faso">Burkina Faso</option>
                                <option value="Burundi">Burundi</option>
                                <option value="Cambodia">Cambodia</option>
                                <option value="Cameroon">Cameroon</option>
                                <option value="Canada">Canada</option>
                                <option value="Cape Verde">Cape Verde</option>
                                <option value="Cayman Islands">Cayman Islands</option>
                                <option value="Central African Republic">Central African Republic</option>
                                <option value="Chad">Chad</option>
                                <option value="Chile">Chile</option>
                                <option value="China">China</option>
                                <option value="Christmas Island">Christmas Island</option>
                                <option value="Cocos [Keeling] Islands">Cocos [Keeling] Islands</option>
                                <option value="Colombia">Colombia</option>
                                <option value="Comoros">Comoros</option>
                                <option value="Cook Islands">Cook Islands</option>
                                <option value="Costa Rica">Costa Rica</option>
                                <option value="Croatia">Croatia</option>
                                <option value="Cuba">Cuba</option>
                                <option value="Curacao">Curacao</option>
                                <option value="Cyprus">Cyprus</option>
                                <option value="Czechia">Czechia</option>
                                <option value="Democratic Republic of the Congo">Democratic Republic of the Congo</option>
                                <option value="Denmark">Denmark</option>
                                <option value="Djibouti">Djibouti</option>
                                <option value="Dominica">Dominica</option>
                                <option value="Dominican Republic">Dominican Republic</option>
                                <option value="East Timor">East Timor</option>
                                <option value="Ecuador">Ecuador</option>
                                <option value="Egypt">Egypt</option>
                                <option value="El Salvador">El Salvador</option>
                                <option value="Equatorial Guinea">Equatorial Guinea</option>
                                <option value="Eritrea">Eritrea</option>
                                <option value="Estonia">Estonia</option>
                                <option value="Ethiopia">Ethiopia</option>
                                <option value="Falkland Islands">Falkland Islands</option>
                                <option value="Faroe Islands">Faroe Islands</option>
                                <option value="Fiji">Fiji</option>
                                <option value="Finland">Finland</option>
                                <option value="France">France</option>
                                <option value="French Guiana">French Guiana</option>
                                <option value="French Polynesia">French Polynesia</option>
                                <option value="French Southern Territories">French Southern Territories</option>
                                <option value="Gabon">Gabon</option>
                                <option value="Gambia">Gambia</option>
                                <option value="Georgia">Georgia</option>
                                <option value="Germany">Germany</option>
                                <option value="Ghana">Ghana</option>
                                <option value="Gibraltar">Gibraltar</option>
                                <option value="Greece">Greece</option>
                                <option value="Greenland">Greenland</option>
                                <option value="Grenada">Grenada</option>
                                <option value="Guadeloupe">Guadeloupe</option>
                                <option value="Guam">Guam</option>
                                <option value="Guatemala">Guatemala</option>
                                <option value="Guernsey">Guernsey</option>
                                <option value="Guinea">Guinea</option>
                                <option value="Guinea-Bissau">Guinea-Bissau</option>
                                <option value="Guyana">Guyana</option>
                                <option value="Haiti">Haiti</option>
                                <option value="Heard Island and McDonald Islands">Heard Island and McDonald Islands</option>
                                <option value="Honduras">Honduras</option>
                                <option value="Hong Kong">Hong Kong</option>
                                <option value="Hungary">Hungary</option>
                                <option value="Iceland">Iceland</option>
                                <option value="India">India</option>
                                <option value="Indonesia">Indonesia</option>
                                <option value="Iran">Iran</option>
                                <option value="Iraq">Iraq</option>
                                <option value="Ireland">Ireland</option>
                                <option value="Isle of Man">Isle of Man</option>
                                <option value="Israel">Israel</option>
                                <option value="Italy">Italy</option>
                                <option value="Ivory Coast">Ivory Coast</option>
                                <option value="Jamaica">Jamaica</option>
                                <option selected="selected" value="Japan">Japan</option>
                                <option value="Jersey">Jersey</option>
                                <option value="Jordan">Jordan</option>
                                <option value="Kazakhstan">Kazakhstan</option>
                                <option value="Kenya">Kenya</option>
                                <option value="Kiribati">Kiribati</option>
                                <option value="Kosovo">Kosovo</option>
                                <option value="Kuwait">Kuwait</option>
                                <option value="Kyrgyzstan">Kyrgyzstan</option>
                                <option value="Laos">Laos</option>
                                <option value="Latvia">Latvia</option>
                                <option value="Lebanon">Lebanon</option>
                                <option value="Lesotho">Lesotho</option>
                                <option value="Liberia">Liberia</option>
                                <option value="Libya">Libya</option>
                                <option value="Liechtenstein">Liechtenstein</option>
                                <option value="Lithuania">Lithuania</option>
                                <option value="Luxembourg">Luxembourg</option>
                                <option value="Macao">Macao</option>
                                <option value="Macedonia">Macedonia</option>
                                <option value="Madagascar">Madagascar</option>
                                <option value="Malawi">Malawi</option>
                                <option value="Malaysia">Malaysia</option>
                                <option value="Maldives">Maldives</option>
                                <option value="Mali">Mali</option>
                                <option value="Malta">Malta</option>
                                <option value="Marshall Islands">Marshall Islands</option>
                                <option value="Martinique">Martinique</option>
                                <option value="Mauritania">Mauritania</option>
                                <option value="Mauritius">Mauritius</option>
                                <option value="Mayotte">Mayotte</option>
                                <option value="Mexico">Mexico</option>
                                <option value="Micronesia">Micronesia</option>
                                <option value="Moldova">Moldova</option>
                                <option value="Monaco">Monaco</option>
                                <option value="Mongolia">Mongolia</option>
                                <option value="Montenegro">Montenegro</option>
                                <option value="Montserrat">Montserrat</option>
                                <option value="Morocco">Morocco</option>
                                <option value="Mozambique">Mozambique</option>
                                <option value="Myanmar [Burma]">Myanmar [Burma]</option>
                                <option value="Namibia">Namibia</option>
                                <option value="Nauru">Nauru</option>
                                <option value="Nepal">Nepal</option>
                                <option value="Netherlands">Netherlands</option>
                                <option value="New Caledonia">New Caledonia</option>
                                <option value="New Zealand">New Zealand</option>
                                <option value="Nicaragua">Nicaragua</option>
                                <option value="Niger">Niger</option>
                                <option value="Nigeria">Nigeria</option>
                                <option value="Niue">Niue</option>
                                <option value="Norfolk Island">Norfolk Island</option>
                                <option value="North Korea">North Korea</option>
                                <option value="Northern Mariana Islands">Northern Mariana Islands</option>
                                <option value="Norway">Norway</option>
                                <option value="Oman">Oman</option>
                                <option value="Pakistan">Pakistan</option>
                                <option value="Palau">Palau</option>
                                <option value="Palestine">Palestine</option>
                                <option value="Panama">Panama</option>
                                <option value="Papua New Guinea">Papua New Guinea</option>
                                <option value="Paraguay">Paraguay</option>
                                <option value="Peru">Peru</option>
                                <option value="Philippines">Philippines</option>
                                <option value="Pitcairn Islands">Pitcairn Islands</option>
                                <option value="Poland">Poland</option>
                                <option value="Portugal">Portugal</option>
                                <option value="Puerto Rico">Puerto Rico</option>
                                <option value="Qatar">Qatar</option>
                                <option value="Republic of the Congo">Republic of the Congo</option>
                                <option value="Romania">Romania</option>
                                <option value="Russia">Russia</option>
                                <option value="Rwanda">Rwanda</option>
                                <option value="Réunion">Réunion</option>
                                <option value="Saint Barthélemy">Saint Barthélemy</option>
                                <option value="Saint Helena">Saint Helena</option>
                                <option value="Saint Kitts and Nevis">Saint Kitts and Nevis</option>
                                <option value="Saint Lucia">Saint Lucia</option>
                                <option value="Saint Martin">Saint Martin</option>
                                <option value="Saint Pierre and Miquelon">Saint Pierre and Miquelon</option>
                                <option value="Saint Vincent and the Grenadines">Saint Vincent and the Grenadines</option>
                                <option value="Samoa">Samoa</option>
                                <option value="San Marino">San Marino</option>
                                <option value="Saudi Arabia">Saudi Arabia</option>
                                <option value="Senegal">Senegal</option>
                                <option value="Serbia">Serbia</option>
                                <option value="Seychelles">Seychelles</option>
                                <option value="Sierra Leone">Sierra Leone</option>
                                <option value="Singapore">Singapore</option>
                                <option value="Sint Maarten">Sint Maarten</option>
                                <option value="Slovakia">Slovakia</option>
                                <option value="Slovenia">Slovenia</option>
                                <option value="Solomon Islands">Solomon Islands</option>
                                <option value="Somalia">Somalia</option>
                                <option value="South Africa">South Africa</option>
                                <option value="South Georgia and the South Sandwich Islands">South Georgia and the South Sandwich Islands</option>
                                <option value="South Korea">South Korea</option>
                                <option value="South Sudan">South Sudan</option>
                                <option value="Spain">Spain</option>
                                <option value="Sri Lanka">Sri Lanka</option>
                                <option value="Sudan">Sudan</option>
                                <option value="Suriname">Suriname</option>
                                <option value="Svalbard and Jan Mayen">Svalbard and Jan Mayen</option>
                                <option value="Swaziland">Swaziland</option>
                                <option value="Sweden">Sweden</option>
                                <option value="Switzerland">Switzerland</option>
                                <option value="Syria">Syria</option>
                                <option value="São Tomé and Príncipe">São Tomé and Príncipe</option>
                                <option value="Taiwan">Taiwan</option>
                                <option value="Tajikistan">Tajikistan</option>
                                <option value="Tanzania">Tanzania</option>
                                <option value="Thailand">Thailand</option>
                                <option value="Togo">Togo</option>
                                <option value="Tokelau">Tokelau</option>
                                <option value="Tonga">Tonga</option>
                                <option value="Trinidad and Tobago">Trinidad and Tobago</option>
                                <option value="Tunisia">Tunisia</option>
                                <option value="Turkey">Turkey</option>
                                <option value="Turkmenistan">Turkmenistan</option>
                                <option value="Turks and Caicos Islands">Turks and Caicos Islands</option>
                                <option value="Tuvalu">Tuvalu</option>
                                <option value="U.S. Minor Outlying Islands">U.S. Minor Outlying Islands</option>
                                <option value="U.S. Virgin Islands">U.S. Virgin Islands</option>
                                <option value="Uganda">Uganda</option>
                                <option value="Ukraine">Ukraine</option>
                                <option value="United Arab Emirates">United Arab Emirates</option>
                                <option value="United Kingdom">United Kingdom</option>
                                <option value="United States">United States</option>
                                <option value="Uruguay">Uruguay</option>
                                <option value="Uzbekistan">Uzbekistan</option>
                                <option value="Vanuatu">Vanuatu</option>
                                <option value="Vatican City">Vatican City</option>
                                <option value="Venezuela">Venezuela</option>
                                <option value="Vietnam">Vietnam</option>
                                <option value="Wallis and Futuna">Wallis and Futuna</option>
                                <option value="Western Sahara">Western Sahara</option>
                                <option value="Yemen">Yemen</option>
                                <option value="Zambia">Zambia</option>
                                <option value="Zimbabwe">Zimbabwe</option>
                            </select>
                        </div>
                    </div>
                    <div class="flex flex-col sm:flex-row space-y-1 sm:items-end ml-">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="postal" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Postal Code')}:*</label>
                            <input
                                type="text"
                                id="postal"
                                value={postalCode}
                                onChange={(e) => setPostalCode(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="prefecture" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Prefecture')}:*</label> 
                            <input
                                type="text"
                                id="prefecture"
                                value={prefecture}
                                onChange={(e) => setPrefecture(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="city" class="text-sm font-semibold text-gray-500 flex flex-start">{t('City')}:*</label> 
                            <input
                                type="text"
                                id="city"
                                value={city}
                                onChange={(e) => setCity(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div class="flex flex-row space-y-1 items-end">
                        <div className='flex flex-1 flex-col p-1'>
                            <label for="streetAddress" class="text-sm font-semibold text-gray-500 flex flex-start">{t('Street Address')}:*</label>
                            <input
                                type="text"
                                id="streetAddress"
                                value={address}
                                onChange={(e) => setAddress(e.target.value)}
                                autoFocus
                                class="px-4 py-2 transition duration-300 border border-gray-300 rounded focus:border-transparent focus:outline-none focus:ring-4 focus:ring-cyan-200"
                            />
                        </div>
                    </div>
                    <div className="flex flex-col space-y-5">
                        <span className="flex items-center space-x-2">
                            <Link to={'/dashboard/changePassword'} className="font-normal text-cyan-400">{t("Change Password?")}</Link>
                        </span>
                    </div>
                    <div>
                    <button
                        onClick={submitHandler}
                        type="button"
                        class="w-full px-4 py-2 text-lg font-semibold text-white transition-colors duration-300 bg-cyan-500 rounded-md shadow hover:bg-cyan-600 focus:outline-none focus:ring-cyan-200 focus:ring-4"
                    >
                        {t('Update Profile')}
                    </button>
                    </div>
                </form>
                </div>
            </div>
        </div>
    )
}

export default Profile