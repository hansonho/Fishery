import React, { useEffect } from 'react';

import Header from '../Share/header';
import Footer from '../Share/footer';

import video1 from '../../media/Photography/DSC_1817.MOV';
import video2 from '../../media/Photography/DSC_1819.MOV';
import video3 from '../../media/Photography/P6060001.MOV';
import video4 from '../../media/Photography/P6060013.MOV';

function Broad() {
    const mediaData = [
        {
            index: 0,
            src: video1,
        },
        {
            index: 1,
            src: video2,
        },
        {
            index: 2,
            src: video3,
        },
        {
            index: 3,
            src: video4,
        },
    ];
    function VideoClick(e) {
        const videoList = document.getElementsByClassName('item');
        for (let i = 0; i < videoList.length; i++) {
            videoList[i].classList.toggle('hide');
            videoList[i].style.pointerEvents = 'none';
        }
        e.previousSibling.style.display = 'inline';
        e.parentNode.classList.add('show');
        e.parentNode.style.pointerEvents = 'auto';
        if (e.currentTime === 0) {
            e.play();
        } else {
            e.currentTime = 0;
            e.play();
        }
        
    }
    function close(e) {
        e.style.display = 'none';
        const videoList = document.getElementsByClassName('item');
        for (let i = 0; i < videoList.length; i++) {
            videoList[i].classList.toggle('hide');
            videoList[i].style.pointerEvents = 'auto';
        }
        document.getElementsByClassName('show')[0].classList.remove('show');
        e.nextElementSibling.pause();
    }
    return (
        <div className="photographypage">
            <Header />
            <div className='photography-info'>
                {mediaData.map((e) => {
                    return (
                        <div className="item" key={e.index}>
                            <span className="close" onClick={(element) => close(element.target)}>X</span>
                            <video className="videoItem" preload="metadata" onClick={(element) => VideoClick(element.target)}>
                                <source src={e.src} type="video/mp4" />
                            </video>
                        </div>
                    );
                })}
            </div>
            <Footer />
        </div>
    );
}

export default Broad;
