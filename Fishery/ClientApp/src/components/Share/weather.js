import React, { useState, useEffect } from 'react';

function Weather() {
    const [weather, setWeather] = useState('氣象資料讀取中');
    const [wind, setWind] = useState('');
    const [humidity, setHumidity] = useState('');
    const [temp, setTemp] = useState('');
    const [pm25, setPm25] = useState('');
    const [pressure, setPressure] = useState('');
    const [windSpeed, setWindSpeed] = useState('');
    const [rain, setRain] = useState('');
    useEffect(() => {
        async function fetchData() {
            const url = 'https://cors-anywhere.herokuapp.com/https://harbor-auth.insynerger.com:9999/auth/oauth/token?client_id=7807fb46-5f7f-11ea-bc55-0242ac130003&client_secret=015de4ec-8adc-492d-84a4-9c66706c79f5&grant_type=password&username=apiuser&password=apiuser@2020&apsystem=ILC';
            const clientId = '7807fb46-5f7f-11ea-bc55-0242ac130003';
            const clientSecret = '015de4ec-8adc-492d-84a4-9c66706c79f5';
            const hash = window.btoa(`${clientId}:${clientSecret}`); 
            const responseToken = await fetch(url, {
                method: 'POST',
                headers: {
                    'Authorization': `Basic ${hash}`,
                    'Accept': 'application/json',
                    'content-type': 'application/json'
                },
            });
            const json = await responseToken.json();

            const wind = 'https://cors-anywhere.herokuapp.com/https://harbor.insynerger.com/api/incommon/v2/devices/sensor/IN21IIMSN-0353010301/';
            const responseWind = await fetch(wind, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${json.access_token}`,
                    'Accept': 'application/json',
                    'content-type': 'application/json'
                },
            });
            const windData = await responseWind.json();
            windData.data.attributes.forEach(element => {
                if (element.id === 401300) {
                    setWind(`風向 ${element.attrValue.value} deg`);
                }
            });

            const humidity = 'https://cors-anywhere.herokuapp.com/https://harbor.insynerger.com/api/incommon/v2/devices/sensor/IN21IIMSN-0353-TEST1/';
            const responseHumidity = await fetch(humidity, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${json.access_token}`,
                    'Accept': 'application/json',
                    'content-type': 'application/json'
                },
            });
            const humidityData = await responseHumidity.json();
            humidityData.data.attributes.forEach(element => {
                if (element.id === 400200) {
                    setHumidity(`濕度 ${element.attrValue.value} %`);
                }
            });

            const temp = 'https://cors-anywhere.herokuapp.com/https://harbor.insynerger.com/api/incommon/v2/devices/sensor/IN21IIMSN-0353-TEST2/';
            const responseTemp = await fetch(temp, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${json.access_token}`,
                    'Accept': 'application/json',
                    'content-type': 'application/json'
                },
            });
            const tempData = await responseTemp.json();
            tempData.data.attributes.forEach(element => {
                if (element.id === 400100) {
                    setTemp(`溫度 ${element.attrValue.value} ℃`);
                }
            });

            const pm25 = 'https://cors-anywhere.herokuapp.com/https://harbor.insynerger.com/api/incommon/v2/devices/sensor/IN21IIMSN-0353-TEST3/';
            const responsePM25 = await fetch(pm25, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${json.access_token}`,
                    'Accept': 'application/json',
                    'content-type': 'application/json'
                },
            });
            const pm25Data = await responsePM25.json();
            pm25Data.data.attributes.forEach(element => {
                if (element.id === 401500) {
                    setPm25(`PM2.5 ${element.attrValue.value} μg/m3`);
                }
            });

            const pressure = 'https://cors-anywhere.herokuapp.com/https://harbor.insynerger.com/api/incommon/v2/devices/sensor/IN21IIMSN-0353-TEST6/';
            const responsePressure = await fetch(pressure, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${json.access_token}`,
                    'Accept': 'application/json',
                    'content-type': 'application/json'
                },
            });
            const pressureData = await responsePressure.json();
            pressureData.data.attributes.forEach(element => {
                if (element.id === 401200) {
                    setPressure(`大氣壓力 ${element.attrValue.value} kg/cm2`);
                }
            });

            const windSpeed = 'https://cors-anywhere.herokuapp.com/https://harbor.insynerger.com/api/incommon/v2/devices/sensor/IN21IIMSN-0353010201/';
            const responseWindSpeed = await fetch(windSpeed, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${json.access_token}`,
                    'Accept': 'application/json',
                    'content-type': 'application/json'
                },
            });
            const windSpeedData = await responseWindSpeed.json();
            windSpeedData.data.attributes.forEach(element => {
                if (element.id === 401400) {
                    setWindSpeed(`風速 ${element.attrValue.value} m/s`);
                }
            });

            const rain = 'https://cors-anywhere.herokuapp.com/https://harbor.insynerger.com/api/incommon/v2/devices/sensor/IN21IIMSN-0353010101/';
            const responseRain = await fetch(rain, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${json.access_token}`,
                    'Accept': 'application/json',
                    'content-type': 'application/json'
                },
            });
            const rainData = await responseRain.json();
            rainData.data.attributes.forEach(element => {
                if (element.id === 402900) {
                    setRain(`雨量 ${element.attrValue.value} mm/hr`);
                }
            });
        }
        fetchData();
    }, []);
    if (wind !== '' && humidity !== '' && temp !== '' && pm25 !== '' && pressure !== '' && windSpeed !== '' && rain !== '') {
        console.log(wind)
        console.log(humidity)
        console.log(temp)
        console.log(pm25)
        console.log(pressure)
        console.log(windSpeed)
        console.log(rain)
        const array = [wind, humidity, temp, pm25, pressure, windSpeed, rain];
        let i = 0;
        setInterval(() => {
            document.getElementById('weatherInfo').textContent = array[i];
            i += 1;
            if (i === array.length) {
                i = 0;
            }
        }, 10000)
        return (
            <>
                <div className="item weather">
                    <span id="weatherInfo"></span>
                </div>
            </>
        );
    }
    return (
        <>
            <div className="item weather">
                <span>{weather}</span>
            </div>
        </>
    );
}

export default Weather;
