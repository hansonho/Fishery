import React, { useState, useEffect } from 'react';

function Datetime() {
    const today = new Date();
    const [year, setYear] = useState(today.getFullYear());
    const [month, setMonth] = useState(today.getMonth() + 1 < 10 ? `0${today.getMonth() + 1}` : today.getMonth() + 1);
    const [date, setDate] = useState(today.getDate());
    const [hour, setHour] = useState(today.getHours() < 10 ? `0${today.getHours()}` : today.getHours());
    const [min, setMin] = useState(today.getMinutes() < 10 ? `0${today.getMinutes()}` : today.getMinutes());
    const [sec, setSec] = useState(today.getSeconds() < 10 ? `0${today.getSeconds()}` : today.getSeconds());

    function getToday() {
        const today = new Date();
        setYear(today.getFullYear());
        setMonth(today.getMonth() + 1 < 10 ? `0${today.getMonth() + 1}` : today.getMonth() + 1);
        setDate(today.getDate());
        setHour(today.getHours() < 10 ? `0${today.getHours()}` : today.getHours());
        setMin(today.getMinutes() < 10 ? `0${today.getMinutes()}` : today.getMinutes());
        setSec(today.getSeconds() < 10 ? `0${today.getSeconds()}` : today.getSeconds());
    }

    useEffect(() => {
        setInterval(() => {
            getToday();
        }, 1000);
    });

    return (
        <>
            <div className="item datetime">
                <span>{`${year}/${month}/${date} ${hour}:${min}:${sec}`}</span>
            </div>
        </>
    );
}

export default Datetime;
