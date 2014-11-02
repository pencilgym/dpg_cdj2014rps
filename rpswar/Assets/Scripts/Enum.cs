enum gState {
	setupscene,
	intro,
	getready,
	choose,
	showresult,
	movetonextscene,
	victory,
};

public enum choice {
	undecided,
	rock,
	scissors,
	paper
};

public enum winner {
	p1wins,
	p2wins,
	tie
}

public enum pState {	// player states
	introrun,
	idle,
	win,
	lose,
	winrun
};
