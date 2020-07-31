import React, { useState, useEffect } from 'react';

function Weather() {
    const [weather, setWeather] = useState('氣象資料讀取中');
    const [weatherData, setWeatherData] = useState([]);
    /*
    const [humidity, setHumidity] = useState('');
    const [temp, setTemp] = useState('');
    const [pm25, setPm25] = useState('');
    const [pressure, setPressure] = useState('');
    const [windSpeed, setWindSpeed] = useState('');
    const [rain, setRain] = useState('');
    */
    useEffect(() => {
        async function fetchData() {
            const response = await fetch('/api/WeatherForecast', {
                method: 'GET',
                headers: new Headers({
                    'Content-Type': 'application/json',
                }),
            });
            const json = await response.json();
            setWeatherData(json.data);
        }
        fetchData();
    }, []);
    if (weatherData.length > 0) {
        /*
        console.log(wind)
        console.log(humidity)
        console.log(temp)
        console.log(pm25)
        console.log(pressure)
        console.log(windSpeed)
        console.log(rain)
        const array = [wind, humidity, temp, pm25, pressure, windSpeed, rain];
        */
        let i = 0;
        setInterval(() => {
            document.getElementById('weatherInfo').textContent = weatherData[i];
            i += 1;
            if (i === weatherData.length) {
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
