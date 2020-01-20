export interface Track {
    name: string,
    artists: string[],
    isPlaying: boolean,
    ytSongLink: string,
}

export interface History {
    currentTrack?: Track,
    tracks: Track[],
}