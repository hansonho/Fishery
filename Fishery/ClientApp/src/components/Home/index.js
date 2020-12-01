import React from 'react';
import { Link } from 'react-router-dom';
import Carousel from './carousel';
import Marquee from './marquee';
import Btnlink from '../Share/btnlink';

import activity from '../../media/HomeBtnImg/activity.png';
import broad from '../../media/HomeBtnImg/broad.png';
import entertainment from '../../media/HomeBtnImg/entertainment.png';
import food from '../../media/HomeBtnImg/food.png';
import photography from '../../media/HomeBtnImg/photography.png';
import sights from '../../media/HomeBtnImg/sights.png';
import parking from '../../media/HomeBtnImg/parking.png';
// import video from '../../media/Carousel/h264.480.60s.mp4';

function Home() {
         
    if (window.screen.width === 1080) {
        const picData = [
            {
                index: 0,
                src: sights,
                alt: 'scenic',
                href: '/scenic.html',
            },
            {
                index: 1,
                src: food,
                alt: 'food',
                href: '/food.html',
            },
            {
                index: 2,
                src: entertainment,
                alt: 'boat',
                href: '/boat.html',
            },
            {
                index: 3,
                src: activity,
                alt: 'activity',
                href: '/activity',
            },
            {
                index: 4,
                src: broad,
                alt: 'livebroadcast',
                href: 'http://210.65.88.130:90/',
                target: '_self',
            },
            {
                index: 5,
                src: photography,
                alt: 'photography',
                href: '/photography',
            },
        ];
        return (
            <div className="homepage">
                <Carousel />
                <Marquee />
                <Btnlink className="homelink-info" picData={picData} />
            </div>
        );
    }
    const picData = [
        {
            index: 0,
            src: sights,
            alt: 'scenic',
            href: '/scenic.html',
        },
        {
            index: 1,
            src: food,
            alt: 'food',
            href: '/food.html',
        },
        {
            index: 2,
            src: entertainment,
            alt: 'boat',
            href: '/boat.html',
        },
        {
            index: 3,
            src: activity,
            alt: 'activity',
            href: '/activity',
        },
        {
            index: 4,
            src: broad,
            alt: 'livebroadcast',
            href: 'http://211.23.78.116:58588/getimage?stream=1&0.9126158323318647',
            target: '_blank',
        },
        {
            index: 5,
            src: photography,
            alt: 'photography',
            href: '/photography',
        },
    ];
    return (
        <div className="homepage">
            <Carousel />
            <Marquee />
            <Btnlink className="homelink-info" picData={picData} />
            <div className="park-btn">
                <Link to="/park">
                    <img src={parking} alt="parking" />
                </Link>
            </div>
        </div>
    );
}

export default Home;