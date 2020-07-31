import React, { useState, useEffect } from 'react';

function Weather() {
    const [weather, setWeather] = useState('氣象資料讀取中');
    const [fetchStatus, setFetchStatus] = useState(false);
    const [weatherData, setWeatherData] = useState([]);
    const [index, setIndex] = useState(0);
    /*
    const [humidity, setHumidity] = useState('');
    const [temp, setTemp] = useState('');
    const [pm25, setPm25] = useState('');
    const [pressure, setPressure] = useState('');
    const [windSpeed, setWindSpeed] = useState('');
    const [rain, setRain] = useState('');
    */

    async function fetchData() {
        const response = await fetch('/api/WeatherForecast', {
            method: 'GET',
            headers: new Headers({
                'Content-Type': 'application/json',
            }),
        });
        const json = await response.json();
        setWeatherData(json.data);
        setFetchStatus(true);
    }

    useEffect(() => {
        if (!fetchStatus) {
            fetchData();
            setFetchStatus(true);
        }
        const fetInterval = setInterval(() => {
            fetchData();
        }, 60000)
        return () => {
            clearInterval(fetInterval);
        }
    }, []);

    useEffect(() => {
        const changeWeatherInfo = setInterval(() => {
            setWeather(weatherData[index]);
            setIndex(index + 1);
            if (index === weatherData.length - 1) {
                setIndex(0);
            } else {
                setIndex(index + 1);
            }
        }, 10000)
        return () => {
            clearInterval(changeWeatherInfo);
        }
    }, [weatherData, index])

    if (weatherData.length === 0) {
        return (
            <>
                <div className="item weather">
                    <span>{weather}</span>
                </div>
            </>
        );
    }

    //if (weatherData.length > 0) {
    //    console.log(weatherData)
    //    /*
    //    console.log(wind)
    //    console.log(humidity)
    //    console.log(temp)
    //    console.log(pm25)
    //    console.log(pressure)
    //    console.log(windSpeed)
    //    console.log(rain)
    //    const array = [wind, humidity, temp, pm25, pressure, windSpeed, rain];
    //    */
    //    let i = 0;
    //    setInterval(() => {
    //        // document.getElementById('weatherInfo').textContent = weatherData[i];
    //        i += 1;
    //        if (i === weatherData.length) {
    //            i = 0;
    //        }
    //        return (
    //            <>
    //                <div className="item weather">
    //                    <span id="weatherInfo">{weatherData[i]}</span>
    //                </div>
    //            </>
    //        );
    //    }, 10000)
    //}
    return (
        <>
            <div className="item weather">
                <span>{weather}</span>
            </div>
        </>
    );
}

export default Weather;
