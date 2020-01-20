import React from 'react';
import { Track } from './History'
import Song from './Song'

type SongListProps = {
    tracks?: Track[],
}

const SongList: React.FunctionComponent<SongListProps> = (props) => {
    const {tracks} = props;
    if (tracks === undefined) {
        return (
            <h2>Loading...</h2>
        )
    }
    else {
        return (
            <div className="songlist">
            {
                tracks.map((track)=> {
                    return (<Song name={track.name} artists={track.artists} link={track.ytSongLink}/>)
                })
            }
            </div>
        )
    }
}

export default SongList;
